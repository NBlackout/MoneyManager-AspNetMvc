using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Bll.Transaction;
using NBlackout.MoneyManager.Dal.Organization;
using NBlackout.MoneyManager.Dal.OrganizationCategory;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.ViewModels.Results;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Configuration.Controllers
{
    public class SuggestionsController : BaseController
    {
        private TransactionManager transactionManager = new TransactionManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Suggestions()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            return View();
        }

        public async Task<ActionResult> SuggestionsPartial()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Suggestions));

            using (var context = new MoneyManagerDbContext())
            {
                var transactions = await transactionManager.SuggestionsAsync();
                var transactionsVm = AutoMapperConfig.Mapper.Map<IEnumerable<SuggestionResultViewModel>>(transactions);

                return PartialView("_Suggestions", transactionsVm);
            }
        }
    }
}