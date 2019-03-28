using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Bll.TransactionForecastHit;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("TransactionForecastHits")]
    public class TransactionForecastHitsController : SecureODataController
    {
        private BaseDao<TransactionForecastHitModel> transactionForecastHitDao = new BaseDao<TransactionForecastHitModel>();
        private TransactionForecastHitManager transactionForecastHitManager = new TransactionForecastHitManager();

        #region Crud

        [EnableQuery(MaxExpansionDepth = 3)]
        public IHttpActionResult Get()
        {
            var accounts = transactionForecastHitDao.Find();

            return Ok(accounts);
        }

        [EnableQuery(MaxExpansionDepth = 3)]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var transactionForecastHit = await transactionForecastHitDao.FindByIdAsync(key);
            if (transactionForecastHit == null)
                return NotFound();

            return Ok(transactionForecastHit);
        }

        public async Task<IHttpActionResult> Post(TransactionForecastHitModel transactionForecastHit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await transactionForecastHitDao.InsertAsync(transactionForecastHit);

            return Created(transactionForecastHit);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<TransactionForecastHitModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await transactionForecastHitDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, TransactionForecastHitModel transactionForecastHitModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != transactionForecastHitModel.Id)
                return BadRequest();

            var result = await transactionForecastHitDao.UpdateAsync(transactionForecastHitModel);
            if (!result)
                return NotFound();

            return Updated(transactionForecastHitModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await transactionForecastHitDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Functions

        [HttpGet]
        public async Task<IHttpActionResult> ReconciliationSuggestions()
        {
            var reconciliationSuggestions = await transactionForecastHitManager.ReconciliationSuggestionsAsync();

            return Ok(reconciliationSuggestions);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transactionForecastHitDao != null)
                {
                    transactionForecastHitDao.Dispose();
                    transactionForecastHitDao = null;
                }

                if (transactionForecastHitManager != null)
                {
                    transactionForecastHitManager.Dispose();
                    transactionForecastHitManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
