using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Dal.Organization;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("Organizations")]
    public class OrganizationsController : SecureODataController
    {
        private OrganizationDao organizationDao = new OrganizationDao();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var organizations = organizationDao.Find();

            return Ok(organizations);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var organization = await organizationDao.FindByIdAsync(key);
            if (organization == null)
                return NotFound();

            return Ok(organization);
        }

        public async Task<IHttpActionResult> Post(OrganizationModel organization)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await organizationDao.InsertAsync(organization);

            return Created(organization);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<OrganizationModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await organizationDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, OrganizationModel organizationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != organizationModel.Id)
                return BadRequest();

            var result = await organizationDao.UpdateAsync(organizationModel);
            if (!result)
                return NotFound();

            return Updated(organizationModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await organizationDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Actions

        [HttpPost]
        public async Task<IHttpActionResult> ReassignTransactionsOrganization(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sourceOrganizationId = (long)parameters["sourceOrganizationId"];
            var targetOrganizationId = (long?)parameters["targetOrganizationId"];

            var result = await organizationDao.ReassignTransactionsOrganization(sourceOrganizationId, targetOrganizationId);
            if (!result)
                return BadRequest();

            return Ok();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (organizationDao != null)
                {
                    organizationDao.Dispose();
                    organizationDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
