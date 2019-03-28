using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Dal.AccountCategory;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("AccountCategories")]
    public class AccountCategoriesController : SecureODataController
    {
        private AccountCategoryDao accountCategoryDao = new AccountCategoryDao();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var accounts = accountCategoryDao.Find();

            return Ok(accounts);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var accountCategory = await accountCategoryDao.FindByIdAsync(key);
            if (accountCategory == null)
                return NotFound();

            return Ok(accountCategory);
        }

        public async Task<IHttpActionResult> Post(AccountCategoryModel accountCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await accountCategoryDao.InsertAsync(accountCategory);

            return Created(accountCategory);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<AccountCategoryModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await accountCategoryDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, AccountCategoryModel accountCategoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != accountCategoryModel.Id)
                return BadRequest();

            var result = await accountCategoryDao.UpdateAsync(accountCategoryModel);
            if (!result)
                return NotFound();

            return Updated(accountCategoryModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await accountCategoryDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Actions

        [HttpPost]
        public async Task<IHttpActionResult> ReassignAccountsCategory(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sourceCategoryId = (long)parameters["sourceCategoryId"];
            var targetCategoryId = (long?)parameters["targetCategoryId"];

            var result = await accountCategoryDao.ReassignAccountsCategory(sourceCategoryId, targetCategoryId);
            if (!result)
                return BadRequest();

            return Ok();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (accountCategoryDao != null)
                {
                    accountCategoryDao.Dispose();
                    accountCategoryDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
