using System;
using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Dal.Account
{
    public class AccountDao : BaseDao<AccountModel>
    {
        public AccountDao() : base()
        {
        }

        public AccountDao(MoneyManagerDbContext context) : base(context)
        {
        }

        public async Task<IQueryable<TransactionModel>> TransactionsOfMonthAsync(long key, int year, int month)
        {
            var account = await FindByIdAsync(key);
            if (account == null)
                throw new ArgumentException("No account found for given key", nameof(key));

            var transactions = from transaction in context.Transactions
                               where transaction.AccountId == key
                                && transaction.Date.Year == year
                                && transaction.Date.Month == month
                               orderby transaction.Date
                               select transaction;

            return transactions;
        }

        public async Task<IQueryable<TransactionModel>> TransactionsOfOrganizationAsync(long key, long organizationId)
        {
            var account = await FindByIdAsync(key);
            if (account == null)
                throw new ArgumentException("No account found for given key", nameof(key));

            var transactions = from transaction in context.Transactions
                               where transaction.AccountId == key
                                && transaction.OrganizationId == organizationId
                               orderby transaction.Date
                               select transaction;

            return transactions;
        }

        public async Task<IQueryable<TransactionForecastHitModel>> TransactionForecastHitsOfMonthAsync(long key, int year, int month, bool? doneFilter)
        {
            var account = await FindByIdAsync(key);
            if (account == null)
                throw new ArgumentException("No account found for given key", nameof(key));

            var transactionForecastHits = from transactionForecastHit in context.TransactionForecastHits
                                          where transactionForecastHit.Forecast.AccountId == key
                                           && transactionForecastHit.Date.Year == year
                                           && transactionForecastHit.Date.Month == month
                                           && (!doneFilter.HasValue || transactionForecastHit.TransactionId.HasValue == doneFilter)
                                          orderby transactionForecastHit.Date
                                          select transactionForecastHit;

            return transactionForecastHits;
        }
    }
}