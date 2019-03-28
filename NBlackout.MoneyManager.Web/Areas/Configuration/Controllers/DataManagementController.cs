using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NBlackout.MoneyManager.Bll.DataManagement;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Configuration.Controllers
{
    public class DataManagementController : BaseController
    {
        private DataManagementManager dataManagementManager = new DataManagementManager();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Import(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null || files.Count() == 0)
                throw new ArgumentNullException(nameof(files));

            var totalTransactionsAdded = 0L;

            using (var context = new MoneyManagerDbContext())
            {
                foreach (var file in files)
                {
                    if (file == null || file.ContentLength == 0)
                        throw new ArgumentNullException(nameof(file));

                    var username = User.Identity.GetUserName();
                    var fileName = file.FileName;
                    byte[] content = null;

                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        content = reader.ReadBytes(file.ContentLength);
                    }

                    var transactionsAdded = await dataManagementManager.ImportAsync(username, fileName, content);

                    totalTransactionsAdded += transactionsAdded;
                }
            }

            return Json(totalTransactionsAdded, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportHistories()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            return View();
        }

        public async Task<ActionResult> ImportHistoriesPartial()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var importHistories = await context.ImportHistories.ToListAsync();
                var importHistoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<ImportHistoryViewModel>>(importHistories);

                return PartialView("_ImportHistories", importHistoriesVm);
            }
        }
    }
}