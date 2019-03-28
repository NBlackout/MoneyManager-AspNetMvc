using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Dal.Customer;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Web.OData.Extensions;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    [ODataRoutePrefix("Customers")]
    public class CustomersController : SecureODataController
    {
        private CustomerDao customerDao = new CustomerDao();

        #region Crud

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var accounts = customerDao.Find();

            return Ok(accounts);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key)
        {
            var customer = await customerDao.FindByIdAsync(key);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        public async Task<IHttpActionResult> Post(CustomerModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await customerDao.InsertAsync(customer);

            return Created(customer);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<CustomerModel> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await customerDao.PatchAsync(key, delta);
            if (entity == null)
                return NotFound();

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] long key, CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != customerModel.Id)
                return BadRequest();

            var result = await customerDao.UpdateAsync(customerModel);
            if (!result)
                return NotFound();

            return Updated(customerModel);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            var result = await customerDao.DeleteAsync(key);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Actions

        [HttpPost]
        public async Task<IHttpActionResult> ReassignAccountsCustomer(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sourceCustomerId = (long)parameters["sourceCustomerId"];
            var targetCustomerId = (long?)parameters["targetCustomerId"];

            var result = await customerDao.ReassignAccountsCustomer(sourceCustomerId, targetCustomerId);
            if (!result)
                return BadRequest();

            return Ok();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (customerDao != null)
                {
                    customerDao.Dispose();
                    customerDao = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
