using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Bll.Account;
using NBlackout.MoneyManager.Dal.Account;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Consultation.Controllers
{
    public class AccountsController : BaseController
    {
        private AccountDao accountDao = new AccountDao();
        private AccountManager accountManager = new AccountManager();

        public ActionResult Index(long? id = 0)
        {
            return View(id);
        }

        public ActionResult List()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            return View();
        }

        public async Task<ActionResult> ListPartial()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var accounts = await context.Accounts.Include(m => m.Category).Include(m => m.Customer).ToListAsync();
                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

                return PartialView("_List", accountsVm);
            }
        }

        public async Task<ActionResult> ActivityPartial(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index), new { id });

            using (var context = new MoneyManagerDbContext())
            {
                var account = await context.Accounts.Include(m => m.Category).Include(m => m.Customer).SingleOrDefaultAsync(t => t.Id == id);
                var accountVm = AutoMapperConfig.Mapper.Map<AccountViewModel>(account);

                var estimatedBalanceByEndOfMonth = await accountManager.EstimatedBalanceByEndOfMonthAsync(id);
                accountVm.EstimatedBalanceByEndOfMonth = estimatedBalanceByEndOfMonth;

                return PartialView("_Activity", accountVm);
            }
        }

        public async Task<ActionResult> Details(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var account = await context.Accounts.Include(m => m.Category).Include(m => m.Customer).SingleOrDefaultAsync(t => t.Id == id);
                var accountVm = AutoMapperConfig.Mapper.Map<AccountViewModel>(account);

                return View(accountVm);
            }
        }

        public async Task<ActionResult> TransactionForecastHitsOfMonthPartial(long accountId, int year, int month, bool? doneFilter)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var query = await accountDao.TransactionForecastHitsOfMonthAsync(accountId, year, month, doneFilter);
                var transactionForecastHits = await query.Include(m => m.Forecast.Organization.Category).Include(m => m.Forecast.Organization.AutomationElements).ToListAsync();
                var transactionForecastHitsVm = AutoMapperConfig.Mapper.Map<IEnumerable<TransactionForecastHitViewModel>>(transactionForecastHits);

                return PartialView("_TransactionForecastHitsOfMonth", transactionForecastHitsVm);
            }
        }
    }
}
