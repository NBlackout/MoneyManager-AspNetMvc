using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Bll.Account;
using NBlackout.MoneyManager.Dal.Account;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Consultation.Controllers
{
    public class DashboardController : BaseController
    {
        private AccountDao accountDao = new AccountDao();
        private AccountManager accountManager = new AccountManager();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Accounts()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var accounts = await context.Accounts.Include(m => m.Category).Include(t => t.Customer).Where(m => m.Category != null && m.Balance > 0).OrderBy(m => m.Category.Label).ThenBy(m => m.Balance).ToListAsync();
                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

                return Json(accountsVm, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Expenditure(long accountId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var expenditure = await accountManager.ExpenditureAsync(accountId);

                return Json(expenditure, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> TransactionsOfOrganization(long accountId, long organizationId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactions = await accountDao.TransactionsOfOrganizationAsync(accountId, organizationId);
                var transactionsVm = AutoMapperConfig.Mapper.Map<IEnumerable<TransactionViewModel>>(transactions).GroupBy(m => m.Date.ToString("yyyy/MM")).OrderBy(m => m.Key).Select(m => new { Date = m.Key, Amount = m.Sum(x => x.Amount) });

                return Json(transactionsVm, JsonRequestBehavior.AllowGet);
            }
        }
    }
}