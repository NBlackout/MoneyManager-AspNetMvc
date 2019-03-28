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
    [ODataRoutePrefix("AutomationElements")]
    public class AutomationElementsController : SecureODataController
    {
        private BaseDao<AutomationElementModel> automationElementDao = new BaseDao<AutomationElementModel>();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var automationElements = automationElementDao.Find();

            return Ok(automationElements);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var automationElement = await automationElementDao.FindByIdAsync(key);
            if (automationElement == null)
                return NotFound();

            return Ok(automationElement);
        }

        public async Task<IHttpActionResult> Post(AutomationElementModel automationElement)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await automationElementDao.InsertAsync(automationElement);

            return Created(automationElement);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<AutomationElementModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await automationElementDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, AutomationElementModel automationElementModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != automationElementModel.Id)
                return BadRequest();

            var result = await automationElementDao.UpdateAsync(automationElementModel);
            if (!result)
                return NotFound();

            return Updated(automationElementModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await automationElementDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (automationElementDao != null)
                {
                    automationElementDao.Dispose();
                    automationElementDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
