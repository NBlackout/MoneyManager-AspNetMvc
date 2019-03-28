using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models.Results;

namespace NBlackout.MoneyManager.Bll.Transaction
{
    public class TransactionManager : IDisposable
    {
        private MoneyManagerDbContext context;

        public TransactionManager()
        {
            context = new MoneyManagerDbContext();
        }

        public async Task<IEnumerable<SuggestionResult>> SuggestionsAsync()
        {
            var suggestions = new List<SuggestionResult>();

            var automationElements = await context.AutomationElements.Include(m => m.Organization.Category).Include(m => m.Account.Customer).Include(t => t.Tags).ToListAsync();
            foreach (var automationElement in automationElements)
            {
                var organization = automationElement.Organization;
                var account = automationElement.Account;
                var customer = account.Customer;
                var tags = automationElement.Tags;

                foreach (var tag in tags)
                {
                    var transactions = context.Transactions.Where(t => !t.OrganizationId.HasValue && t.Label.Contains(tag.Label) && t.AccountId == automationElement.AccountId && t.Amount >= automationElement.MinAmount && t.Amount <= automationElement.MaxAmount);
                    foreach (var transaction in transactions)
                    {
                        var suggestion = new SuggestionResult
                        {
                            Category = organization.Category.Type,
                            OrganizationId = organization.Id,
                            OrganizationLabel = organization.Label,
                            CustomerId = customer.Id,
                            CustomerFullName = String.Concat(customer.FirstName, " ", customer.LastName),
                            AccountId = account.Id,
                            AccountTitle = account.Title,
                            TransactionId = transaction.Id,
                            TransactionDate = transaction.Date,
                            TransactionLabel = transaction.Label,
                            TransactionAmount = transaction.Amount
                        };

                        suggestions.Add(suggestion);
                    }
                }
            }

            return suggestions;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        #endregion
    }
}
