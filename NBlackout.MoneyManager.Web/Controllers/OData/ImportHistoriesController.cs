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
    [ODataRoutePrefix("ImportHistories")]
    public class ImportHistoriesController : SecureODataController
    {
        private BaseDao<ImportHistoryModel> importHistoryDao = new BaseDao<ImportHistoryModel>();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var accounts = importHistoryDao.Find();

            return Ok(accounts);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var importHistory = await importHistoryDao.FindByIdAsync(key);
            if (importHistory == null)
                return NotFound();

            return Ok(importHistory);
        }

        public async Task<IHttpActionResult> Post(ImportHistoryModel importHistory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await importHistoryDao.InsertAsync(importHistory);

            return Created(importHistory);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<ImportHistoryModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await importHistoryDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, ImportHistoryModel importHistoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != importHistoryModel.Id)
                return BadRequest();

            var result = await importHistoryDao.UpdateAsync(importHistoryModel);
            if (!result)
                return NotFound();

            return Updated(importHistoryModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await importHistoryDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (importHistoryDao != null)
                {
                    importHistoryDao.Dispose();
                    importHistoryDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
