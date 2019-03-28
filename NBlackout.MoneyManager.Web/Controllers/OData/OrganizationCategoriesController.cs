using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Dal.OrganizationCategory;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("OrganizationCategories")]
    public class OrganizationCategoriesController : SecureODataController
    {
        private OrganizationCategoryDao organizationCategoryDao = new OrganizationCategoryDao();

        #region Crud

        [EnableQuery(MaxExpansionDepth = 3)]
        public IHttpActionResult Get()
        {
            var organizationCategories = organizationCategoryDao.Find();

            return Ok(organizationCategories);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var organizationCategory = await organizationCategoryDao.FindByIdAsync(key);
            if (organizationCategory == null)
                return NotFound();

            return Ok(organizationCategory);
        }

        public async Task<IHttpActionResult> Post(OrganizationCategoryModel organizationCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await organizationCategoryDao.InsertAsync(organizationCategory);

            return Created(organizationCategory);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<OrganizationCategoryModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await organizationCategoryDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, OrganizationCategoryModel organizationCategoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != organizationCategoryModel.Id)
                return BadRequest();

            var result = await organizationCategoryDao.UpdateAsync(organizationCategoryModel);
            if (!result)
                return NotFound();

            return Updated(organizationCategoryModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await organizationCategoryDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Actions

        [HttpPost]
        public async Task<IHttpActionResult> ReassignOrganizationsCategory(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sourceCategoryId = (long)parameters["sourceCategoryId"];
            var targetCategoryId = (long)parameters["targetCategoryId"];

            var result = await organizationCategoryDao.ReassignOrganizationsCategory(sourceCategoryId, targetCategoryId);
            if (!result)
                return BadRequest();

            return Ok();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (organizationCategoryDao != null)
                {
                    organizationCategoryDao.Dispose();
                    organizationCategoryDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
