using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("TransactionForecasts")]
    public class TransactionForecastsController : SecureODataController
    {
        private BaseDao<TransactionForecastModel> transactionForecastDao = new BaseDao<TransactionForecastModel>();

        #region Crud

        [EnableQuery(MaxExpansionDepth = 3)]
        public IHttpActionResult Get()
        {
            var transactionForecasts = transactionForecastDao.Find();

            return Ok(transactionForecasts);
        }

        [EnableQuery(MaxExpansionDepth = 3)]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var transactionForecast = await transactionForecastDao.FindByIdAsync(key);
            if (transactionForecast == null)
                return NotFound();

            return Ok(transactionForecast);
        }

        public async Task<IHttpActionResult> Post(TransactionForecastModel transactionForecast)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await transactionForecastDao.InsertAsync(transactionForecast);

            return Created(transactionForecast);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<TransactionForecastModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await transactionForecastDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, TransactionForecastModel transactionForecastModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != transactionForecastModel.Id)
                return BadRequest();

            var result = await transactionForecastDao.UpdateAsync(transactionForecastModel);
            if (!result)
                return NotFound();

            return Updated(transactionForecastModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await transactionForecastDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transactionForecastDao != null)
                {
                    transactionForecastDao.Dispose();
                    transactionForecastDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
