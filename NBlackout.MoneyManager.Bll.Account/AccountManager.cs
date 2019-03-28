using System;
using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Dal.Account;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models.Results;

namespace NBlackout.MoneyManager.Bll.Account
{
    public class AccountManager : IDisposable
    {
        private MoneyManagerDbContext context;
        private AccountDao accountDao;

        public AccountManager()
        {
            context = new MoneyManagerDbContext();
            accountDao = new AccountDao(context);
        }

        public async Task<IQueryable<ExpenditureResult>> ExpenditureAsync(long key)
        {
            var account = await accountDao.FindByIdAsync(key);
            if (account == null)
                throw new ArgumentException("No account found for given key", nameof(key));

            var now = DateTime.Now;
            var expenditure = from transaction in context.Transactions
                              where transaction.AccountId == key
                                && transaction.Organization != null
                                && transaction.Date.Year == now.Year
                                && transaction.Date.Month == now.Month
                              group transaction by new
                              {
                                  CategoryLabel = transaction.Organization.Category.Label,
                                  OrganizationLabel = transaction.Organization.Label,
                                  OrganizationId = transaction.OrganizationId
                              } into grp
                              orderby grp.Key.CategoryLabel, grp.Key.OrganizationLabel
                              select new ExpenditureResult
                              {
                                  OrganizationId = grp.Key.OrganizationId,
                                  OrganizationLabel = grp.Key.OrganizationLabel,
                                  CategoryLabel = grp.Key.CategoryLabel,
                                  Amount = grp.Sum(t => t.Amount)
                              };

            return expenditure;
        }

        public async Task<decimal> EstimatedBalanceByEndOfMonthAsync(long key)
        {
            var account = await accountDao.FindByIdAsync(key);
            if (account == null)
                throw new ArgumentException("No account found for given key", nameof(key));

            var now = DateTime.Now;
            var year = now.Year;
            var month = now.Month;
            var day = now.Day;
            var daysInMonth = DateTime.DaysInMonth(year, month);

            // Account balance
            var balance = account.Balance;

            // Pending transaction forecast hits amount
            var transactionForecastHits = await accountDao.TransactionForecastHitsOfMonthAsync(key, year, month, null);
            var transactionForecastHitsTotal = transactionForecastHits.Where(t => !t.TransactionId.HasValue).ToList().Sum(t => t.Forecast.AutomationElement?.Amount ?? 0m);

            // Estimated not regular transactions amount by the end of month
            var transactions = await accountDao.TransactionsOfMonthAsync(key, year, month);
            var transactionForecastHitsTransactionIds = transactionForecastHits.Select(t => t.TransactionId ?? 0).ToList();
            var transactionsTotal = transactions.Where(t => t.Amount <= 0 && t.Description != "TODO" && !transactionForecastHitsTransactionIds.Contains(t.Id)).ToList().Sum(t => t.Amount);
            var estimatedTransactionAmounts = transactionsTotal / day * (daysInMonth - day);

            var estimatedBalance = balance + transactionForecastHitsTotal + estimatedTransactionAmounts;

            return estimatedBalance;
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

                if (accountDao != null)
                {
                    accountDao.Dispose();
                    accountDao = null;
                }
            }
        }

        #endregion
    }
}
