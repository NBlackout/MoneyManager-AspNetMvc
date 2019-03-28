using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Bll.Transaction;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("Transactions")]
    public class TransactionsController : SecureODataController
    {
        private BaseDao<TransactionModel> transactionDao = new BaseDao<TransactionModel>();
        private TransactionManager transactionManager = new TransactionManager();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var transactions = transactionDao.Find();

            return Ok(transactions);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var transaction = await transactionDao.FindByIdAsync(key);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        public async Task<IHttpActionResult> Post(TransactionModel transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await transactionDao.InsertAsync(transaction);

            return Created(transaction);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<TransactionModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await transactionDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, TransactionModel transactionModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != transactionModel.Id)
                return BadRequest();

            var result = await transactionDao.UpdateAsync(transactionModel);
            if (!result)
                return NotFound();

            return Updated(transactionModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await transactionDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Functions

        [HttpGet]
        public async Task<IHttpActionResult> Suggestions()
        {
            var suggestions = await transactionManager.SuggestionsAsync();

            return Ok(suggestions);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transactionDao != null)
                {
                    transactionDao.Dispose();
                    transactionDao = null;
                }

                if (transactionManager != null)
                {
                    transactionManager.Dispose();
                    transactionManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
