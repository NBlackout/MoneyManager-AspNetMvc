using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Bll.TransactionForecastHit;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.ViewModels.Results;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Configuration.Controllers
{
    public class PlanningController : BaseController
    {
        private TransactionForecastHitManager transactionForecastHitManager = new TransactionForecastHitManager();

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
                var reconciliationSuggestions = await transactionForecastHitManager.ReconciliationSuggestionsAsync();
                var reconciliationSuggestionsVm = AutoMapperConfig.Mapper.Map<IEnumerable<ReconciliationSuggestionResultViewModel>>(reconciliationSuggestions);

                return PartialView("_Suggestions", reconciliationSuggestionsVm);
            }
        }

        public ActionResult ForecastHits()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            return View();
        }

        public async Task<ActionResult> ForecastHitsPartial()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(ForecastHits));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecasts = await context.TransactionForecasts.Include(t => t.Account).Include(t => t.Organization.Category).Include(t => t.Organization.AutomationElements).Include(t => t.Hits).ToListAsync();
                var transactionForecastsVm = AutoMapperConfig.Mapper.Map<IEnumerable<TransactionForecastViewModel>>(transactionForecasts);

                return View("_ForecastHits", transactionForecastsVm);
            }
        }

        #region Forecast

        public async Task<ActionResult> CreateForecast()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();
                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();

                var accountsVm = AutoMapperConfig.Mapper.Map<ICollection<AccountViewModel>>(accounts);
                var organizationsVm = AutoMapperConfig.Mapper.Map<ICollection<OrganizationViewModel>>(organizations);

                var viewModel = new TransactionForecastViewModel
                {
                    Accounts = accountsVm,
                    Organizations = organizationsVm
                };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> EditForecast(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecast = await context.TransactionForecasts.FindAsync(id);
                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();
                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();

                var accountsVm = AutoMapperConfig.Mapper.Map<ICollection<AccountViewModel>>(accounts);
                var organizationsVm = AutoMapperConfig.Mapper.Map<ICollection<OrganizationViewModel>>(organizations);

                var transactionForecastVm = AutoMapperConfig.Mapper.Map<TransactionForecastViewModel>(transactionForecast);
                transactionForecastVm.Accounts = accountsVm;
                transactionForecastVm.Organizations = organizationsVm;

                return View(transactionForecastVm);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveForecast(TransactionForecastViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    var transactionForecast = AutoMapperConfig.Mapper.Map<TransactionForecastModel>(viewModel);

                    if (viewModel.Id == default(long))
                        context.TransactionForecasts.Add(transactionForecast);
                    else
                        context.Entry(transactionForecast).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();
                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();

                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                viewModel.Accounts = accountsVm;
                viewModel.Organizations = organizationsVm;

                return View((viewModel.Id == default(long)) ? nameof(CreateForecast) : nameof(EditForecast), viewModel);
            }
        }

        public async Task<ActionResult> ToggleForecastAutoReschedule(long id, bool toggle)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecast = await context.TransactionForecasts.FindAsync(id);
                transactionForecast.AutoReschedule = toggle;

                context.Entry(transactionForecast).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DeleteForecast(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecast = await context.TransactionForecasts.FindAsync(id);
                var transactionForecastVm = AutoMapperConfig.Mapper.Map<TransactionForecastViewModel>(transactionForecast);

                return View(transactionForecastVm);
            }
        }

        public async Task<ActionResult> ConfirmDeleteForecast(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecast = await context.TransactionForecasts.FindAsync(id);
                context.Entry(transactionForecast).State = EntityState.Deleted;

                await context.SaveChangesAsync();

                return View(nameof(DeleteForecast));
            }
        }

        #endregion

        #region ForecastHit

        public async Task<ActionResult> CreateForecastHit(long transactionForecastId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecast = await context.TransactionForecasts.Include(m => m.Account).Include(t => t.Organization.Category).Include(t => t.Organization.AutomationElements).SingleOrDefaultAsync(t => t.Id == transactionForecastId);
                var transactionForecastVm = AutoMapperConfig.Mapper.Map<TransactionForecastViewModel>(transactionForecast);

                var viewModel = new TransactionForecastHitViewModel
                {
                    ForecastId = transactionForecastVm.Id,
                    Forecast = transactionForecastVm,
                    Date = DateTime.Now
                };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> CreateForecastHitFromTransactionId(long transactionId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transaction = await context.Transactions.Include(m => m.Account).Include(m => m.Organization.Category).Include(m => m.Organization.AutomationElements).SingleOrDefaultAsync(t => t.Id == transactionId);
                var transactionForecast = await context.TransactionForecasts.Include(m => m.Account).Include(m => m.Organization.Category).Include(m => m.Organization.AutomationElements).SingleOrDefaultAsync(t => t.AccountId == transaction.AccountId && t.OrganizationId == transaction.OrganizationId);

                if (transactionForecast == null)
                {
                    transactionForecast = new TransactionForecastModel
                    {
                        AccountId = transaction.AccountId,
                        Account = transaction.Account,
                        OrganizationId = transaction.OrganizationId.Value,
                        Organization = transaction.Organization,
                    };
                }

                var transactionVm = AutoMapperConfig.Mapper.Map<TransactionViewModel>(transaction);
                var transactionForecastVm = AutoMapperConfig.Mapper.Map<TransactionForecastViewModel>(transactionForecast);

                var viewModel = new TransactionForecastHitViewModel
                {
                    ForecastId = transactionForecastVm.Id,
                    Forecast = transactionForecastVm,
                    Date = transactionVm.Date.AddMonths(1)
                };

                ModelState.Remove(nameof(transactionId));

                return View(nameof(CreateForecastHitFromTransactionId), viewModel);
            }
        }

        public async Task<ActionResult> EditForecastHit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecastHit = await context.TransactionForecastHits.Include(m => m.Forecast.Account.Category).Include(m => m.Forecast.Organization.Category).Include(m => m.Forecast.Organization.AutomationElements).SingleOrDefaultAsync(t => t.Id == id);
                var transactionForecastHitVm = AutoMapperConfig.Mapper.Map<TransactionForecastHitViewModel>(transactionForecastHit);

                return View(transactionForecastHitVm);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveForecastHit(TransactionForecastHitViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    var transactionForecastHit = AutoMapperConfig.Mapper.Map<TransactionForecastHitModel>(viewModel);

                    if (viewModel.Id == default(long))
                    {
                        if (viewModel.ForecastId == default(long))
                        {
                            var transactionForecast = AutoMapperConfig.Mapper.Map<TransactionForecastModel>(viewModel.Forecast);

                            context.TransactionForecasts.Add(transactionForecast);
                            await context.SaveChangesAsync();

                            transactionForecastHit.ForecastId = transactionForecast.Id;
                        }

                        context.TransactionForecastHits.Add(transactionForecastHit);
                    }
                    else
                    {
                        context.Entry(transactionForecastHit).State = EntityState.Modified;
                    }

                    await context.SaveChangesAsync();

                    return new EmptyResult();
                }

                FlagBadRequest();

                return View((viewModel.Id == default(long)) ? nameof(CreateForecastHit) : nameof(EditForecastHit), viewModel);
            }
        }

        public async Task<ActionResult> DeleteForecastHit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecastHit = await context.TransactionForecastHits.FindAsync(id);
                var transactionForecastHitVm = AutoMapperConfig.Mapper.Map<TransactionForecastHitViewModel>(transactionForecastHit);

                return View(transactionForecastHitVm);
            }
        }

        public async Task<ActionResult> ConfirmDeleteForecastHit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecastHit = await context.TransactionForecastHits.FindAsync(id);
                context.Entry(transactionForecastHit).State = EntityState.Deleted;

                await context.SaveChangesAsync();

                return View(nameof(DeleteForecastHit));
            }
        }

        public async Task<ActionResult> UpdateTransactionForecastHits(UpdateTransactionForecastHitTransactionViewModel[] viewModels)
        {
            if (viewModels == null)
                throw new ArgumentNullException(nameof(viewModels));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            foreach (var viewModel in viewModels)
                await UpdateTransactionForecastHit(viewModel);

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> UpdateTransactionForecastHit(UpdateTransactionForecastHitTransactionViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactionForecastHit = await context.TransactionForecastHits.FindAsync(viewModel.TransactionForecastHitId);
                transactionForecastHit.TransactionId = viewModel.TransactionId;

                context.Entry(transactionForecastHit).State = EntityState.Modified;

                var transactionForecastId = transactionForecastHit.ForecastId;
                var transactionForecast = await context.TransactionForecasts.Include(t => t.Hits).SingleOrDefaultAsync(t => t.Id == transactionForecastId);
                if (transactionForecast.AutoReschedule)
                {
                    var nextScheduleDate = transactionForecastHit.Date.AddMonths(1);
                    var latestScheduleDate = transactionForecast.Hits.Max(t => t.Date);

                    if (nextScheduleDate > latestScheduleDate)
                    {
                        var nextSchedule = new TransactionForecastHitModel
                        {
                            ForecastId = transactionForecastId,
                            Date = nextScheduleDate
                        };

                        context.TransactionForecastHits.Add(nextSchedule);
                    }
                }

                await context.SaveChangesAsync();
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}