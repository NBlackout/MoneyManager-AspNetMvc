using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Bll.Account;
using NBlackout.MoneyManager.Dal.Account;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("Accounts")]
    public class AccountsController : SecureODataController
    {
        private AccountDao accountDao = new AccountDao();
        private AccountManager accountManager = new AccountManager();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var accounts = accountDao.Find();

            return Ok(accounts);
        }

        [EnableQuery(MaxExpansionDepth = 3)]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var account = await accountDao.FindByIdAsync(key);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        public async Task<IHttpActionResult> Post(AccountModel account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await accountDao.InsertAsync(account);

            return Created(account);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<AccountModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await accountDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, AccountModel accountModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != accountModel.Id)
                return BadRequest();

            var result = await accountDao.UpdateAsync(accountModel);
            if (!result)
                return NotFound();

            return Updated(accountModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await accountDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Functions

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> TransactionsOfMonth([FromODataUri] long key, int year, int month)
        {
            var transactions = await accountDao.TransactionsOfMonthAsync(key, year, month);

            return Ok(transactions);
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> TransactionsOfOrganization([FromODataUri] long key, long organizationId)
        {
            var transactions = await accountDao.TransactionsOfOrganizationAsync(key, organizationId);

            return Ok(transactions);
        }

        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 4)]
        public async Task<IHttpActionResult> TransactionForecastHitsOfMonth([FromODataUri] long key, int year, int month, bool? doneFilter)
        {
            var transactionForecastHits = await accountDao.TransactionForecastHitsOfMonthAsync(key, year, month, doneFilter);

            return Ok(transactionForecastHits);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Expenditure([FromODataUri] long key)
        {
            var expenditure = await accountManager.ExpenditureAsync(key);

            return Ok(expenditure);
        }

        [HttpGet]
        public async Task<IHttpActionResult> EstimatedBalanceByEndOfMonth([FromODataUri] long key)
        {
            var estimatedBalance = await accountManager.EstimatedBalanceByEndOfMonthAsync(key);

            return Ok(estimatedBalance);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (accountDao != null)
                {
                    accountDao.Dispose();
                    accountDao = null;
                }

                if (accountManager != null)
                {
                    accountManager.Dispose();
                    accountManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
