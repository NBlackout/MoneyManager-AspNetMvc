using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models.Results;

namespace NBlackout.MoneyManager.Bll.TransactionForecastHit
{
    public class TransactionForecastHitManager : IDisposable
    {
        private MoneyManagerDbContext context;

        public TransactionForecastHitManager()
        {
            context = new MoneyManagerDbContext();
        }

        public async Task<IEnumerable<ReconciliationSuggestionResult>> ReconciliationSuggestionsAsync()
        {
            var reconciliationSuggestions = new List<ReconciliationSuggestionResult>();

            context.DisableFilter("Account_Enabled");
            var transactionForecastHits = await context.TransactionForecastHits.Include(t => t.Forecast.Organization.AutomationElements).Include(t => t.Forecast.Account).Where(t => !t.TransactionId.HasValue).ToListAsync();
            foreach (var transactionForecastHit in transactionForecastHits)
            {
                var transactionForecastHitDate = transactionForecastHit.Date;
                var transactionForecast = transactionForecastHit.Forecast;
                var transactionForecastOrganization = transactionForecast.Organization;
                var transactionForecastAutomationElement = transactionForecast.AutomationElement;

                var transactions = await context.Transactions.Where(t => t.AccountId == transactionForecast.AccountId && t.OrganizationId == transactionForecastOrganization.Id && t.Date.Year == transactionForecastHitDate.Year && t.Date.Month == transactionForecastHitDate.Month).ToListAsync();
                var transaction = transactions.FirstOrDefault(t => !reconciliationSuggestions.Any(m => m.TransactionId == t.Id));
                if (transaction != null)
                {
                    var reconciliationSuggestion = new ReconciliationSuggestionResult
                    {
                        TransactionForecastHitId = transactionForecastHit.Id,
                        TransactionForecastAccountTitle = transactionForecast.Account.Title,
                        TransactionForecastOrganizationLabel = transactionForecastOrganization.Label,
                        TransactionForecastOrganizationAmount = transactionForecastAutomationElement?.Amount,
                        TransactionForecastOrganizationTolerance = transactionForecastAutomationElement?.Tolerance,
                        TransactionForecastHitDate = transactionForecastHit.Date,
                        TransactionId = transaction.Id,
                        TransactionLabel = transaction.Label,
                        TransactionDate = transaction.Date,
                        TransactionAmount = transaction.Amount
                    };

                    reconciliationSuggestions.Add(reconciliationSuggestion);
                }
            }

            return reconciliationSuggestions;
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
