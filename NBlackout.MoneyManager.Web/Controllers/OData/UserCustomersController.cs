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
    [ODataRoutePrefix("UserCustomers")]
    public class UserCustomersController : SecureODataController
    {
        private BaseDao<UserCustomerModel> userCustomerDao = new BaseDao<UserCustomerModel>();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var userCustomers = userCustomerDao.Find();

            return Ok(userCustomers);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var userCustomer = await userCustomerDao.FindByIdAsync(key);
            if (userCustomer == null)
                return NotFound();

            return Ok(userCustomer);
        }

        public async Task<IHttpActionResult> Post(UserCustomerModel userCustomer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await userCustomerDao.InsertAsync(userCustomer);

            return Created(userCustomer);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<UserCustomerModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await userCustomerDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, UserCustomerModel userCustomerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != userCustomerModel.Id)
                return BadRequest();

            var result = await userCustomerDao.UpdateAsync(userCustomerModel);
            if (!result)
                return NotFound();

            return Updated(userCustomerModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await userCustomerDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (userCustomerDao != null)
                {
                    userCustomerDao.Dispose();
                    userCustomerDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
