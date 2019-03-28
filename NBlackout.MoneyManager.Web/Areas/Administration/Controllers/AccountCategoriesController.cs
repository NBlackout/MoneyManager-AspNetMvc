using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Dal.AccountCategory;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Administration.Controllers
{
    public class AccountCategoriesController : BaseController
    {
        private AccountCategoryDao accountCategoryDao = new AccountCategoryDao();

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
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var accountCategories = await context.AccountCategories.ToListAsync();
                var accountCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountCategoryViewModel>>(accountCategories);

                return PartialView("_List", accountCategoriesVm);
            }
        }

        public ActionResult Create()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            return View();
        }

        public async Task<ActionResult> Edit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var accountCategory = await context.AccountCategories.FindAsync(id);
                var accountCategoryVm = AutoMapperConfig.Mapper.Map<AccountCategoryViewModel>(accountCategory);

                return View(accountCategoryVm);
            }
        }

        public ActionResult Delete(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var accountCategories = context.AccountCategories.Where(t => t.Id != id);
                var accountCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountCategoryViewModel>>(accountCategories);
                var viewModel = new ReassignAccountCategoryViewModel { SourceCategoryId = id, Categories = accountCategoriesVm };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> ConfirmDelete(ReassignAccountCategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var accountCategories = context.AccountCategories.Where(t => t.Id != viewModel.SourceCategoryId);
                var accountCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountCategoryViewModel>>(accountCategories);

                if (ModelState.IsValid)
                {
                    await accountCategoryDao.ReassignAccountsCategory(viewModel.SourceCategoryId, viewModel.TargetCategoryId);

                    var accountCategory = await context.AccountCategories.FindAsync(viewModel.SourceCategoryId);
                    context.Entry(accountCategory).State = EntityState.Deleted;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                return View(nameof(Delete), new ReassignAccountCategoryViewModel { SourceCategoryId = viewModel.SourceCategoryId, Categories = accountCategoriesVm });
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(AccountCategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    var accountCategory = AutoMapperConfig.Mapper.Map<AccountCategoryModel>(viewModel);

                    if (viewModel.Id == default(long))
                        context.AccountCategories.Add(accountCategory);
                    else
                        context.Entry(accountCategory).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                return View((viewModel.Id == default(long)) ? nameof(Create) : nameof(Edit), viewModel);
            }
        }
    }
}