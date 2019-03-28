using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityFramework.DynamicFilters;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Configuration.Controllers
{
    public class AccountsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
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
                context.DisableFilter("Account_Enabled");

                var accounts = await context.Accounts.Include(m => m.Category).Include(m => m.Customer.UserCustomers).ToListAsync();
                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

                return PartialView("_List", accountsVm);
            }
        }

        public async Task<ActionResult> Edit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var categories = await context.AccountCategories.ToListAsync();
                var categoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountCategoryViewModel>>(categories);

                var customers = await context.Customers.ToListAsync();
                var customersVm = AutoMapperConfig.Mapper.Map<IEnumerable<CustomerViewModel>>(customers);

                var account = await context.Accounts.Include(m => m.Category).Include(m => m.Customer).SingleOrDefaultAsync(t => t.Id == id);
                var accountVm = AutoMapperConfig.Mapper.Map<AccountViewModel>(account);
                accountVm.Categories = categoriesVm;
                accountVm.Customers = customersVm;

                return View(accountVm);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(AccountViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var categories = await context.AccountCategories.ToListAsync();
                var categoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountCategoryViewModel>>(categories);

                var customers = await context.Customers.ToListAsync();
                var customersVm = AutoMapperConfig.Mapper.Map<IEnumerable<CustomerViewModel>>(customers);

                if (ModelState.IsValid)
                {
                    var account = AutoMapperConfig.Mapper.Map<AccountModel>(viewModel);

                    context.Entry(account).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                viewModel.Categories = categoriesVm;
                viewModel.Customers = customersVm;

                return View(nameof(Edit), viewModel);
            }
        }
    }
}