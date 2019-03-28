using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.AspNet.Identity;
using NBlackout.Framework.Web.OData.Controllers;
using NBlackout.MoneyManager.Bll.DataManagement;

namespace NBlackout.MoneyManager.Web.Controllers.OData
{
    public class DataManagementController : SecureODataController
    {
        private DataManagementManager dataManagementManager = new DataManagementManager();

        [HttpPost]
        [EnableQuery]
        [ODataRoute("Import")]
        public async Task<IHttpActionResult> Import(ODataActionParameters parameters)
        {
            var username = User.Identity.GetUserName();
            var fileName = (string)parameters["fileName"];
            var content = (byte[])parameters["content"];

            var transactionsAdded = await dataManagementManager.ImportAsync(username, fileName, content);

            return Ok(transactionsAdded);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dataManagementManager != null)
                {
                    dataManagementManager.Dispose();
                    dataManagementManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
