using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Dal.OrganizationCategory;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Administration.Controllers
{
    public class OrganizationCategoriesController : BaseController
    {
        private OrganizationCategoryDao organizationCategoryDao = new OrganizationCategoryDao();

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
                var organizationCategories = await context.OrganizationCategories.Include(t => t.Categories).ToListAsync();
                var organizationCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(organizationCategories);

                return PartialView("_List", organizationCategoriesVm);
            }
        }

        public async Task<ActionResult> Create(long? categoryId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            var viewModel = new OrganizationCategoryViewModel();

            using (var context = new MoneyManagerDbContext())
            {
                var organizationCategories = await context.OrganizationCategories.Include(t => t.Category).Where(t => !categoryId.HasValue || t.Id != categoryId.Value).ToListAsync();
                var organizationCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(organizationCategories);

                viewModel.Categories = organizationCategoriesVm;

                if (categoryId.HasValue)
                {
                    var organizationCategory = await context.OrganizationCategories.Include(t => t.Category).SingleOrDefaultAsync(t => t.Id == categoryId);
                    var organizationCategoryVm = AutoMapperConfig.Mapper.Map<OrganizationCategoryViewModel>(organizationCategory);

                    viewModel.CategoryId = categoryId;
                    viewModel.Category = organizationCategoryVm;
                    viewModel.Type = organizationCategoryVm.Type;
                    viewModel.Recurrent = organizationCategoryVm.Recurrent;
                }
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Edit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var organizationCategory = await context.OrganizationCategories.Include(t => t.Category).SingleOrDefaultAsync(t => t.Id == id);
                var organizationCategoryVm = AutoMapperConfig.Mapper.Map<OrganizationCategoryViewModel>(organizationCategory);

                var organizationCategories = await context.OrganizationCategories.Include(t => t.Category).Where(t => t.Id != id).ToListAsync();
                var organizationCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(organizationCategories);
                organizationCategoryVm.Categories = organizationCategoriesVm;

                return View(organizationCategoryVm);
            }
        }

        public ActionResult Delete(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var organizationCategories = context.OrganizationCategories.Where(t => t.Id != id);
                var organizationCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(organizationCategories);
                var viewModel = new ReassignOrganizationCategoryViewModel { SourceCategoryId = id, Categories = organizationCategoriesVm };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> ConfirmDelete(ReassignOrganizationCategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var organizationCategories = context.OrganizationCategories.Where(t => t.Id != viewModel.SourceCategoryId);
                var organizationCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(organizationCategories);

                if (ModelState.IsValid)
                {
                    await organizationCategoryDao.ReassignOrganizationsCategory(viewModel.SourceCategoryId, viewModel.TargetCategoryId);

                    var organizationCategory = await context.OrganizationCategories.FindAsync(viewModel.SourceCategoryId);
                    context.Entry(organizationCategory).State = EntityState.Deleted;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                return View(nameof(Delete), new ReassignOrganizationCategoryViewModel { SourceCategoryId = viewModel.SourceCategoryId, Categories = organizationCategoriesVm });
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(OrganizationCategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    var organizationCategory = AutoMapperConfig.Mapper.Map<OrganizationCategoryModel>(viewModel);

                    if (viewModel.Id == default(long))
                    {
                        var organization = new OrganizationModel { Label = "All" };
                        organizationCategory.Organizations.Add(organization);

                        context.OrganizationCategories.Add(organizationCategory);
                    }
                    else
                    {
                        context.Entry(organizationCategory).State = EntityState.Modified;
                    }

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                var organizationCategories = await context.OrganizationCategories.Include(t => t.Category).Where(t => t.Id != viewModel.Id).ToListAsync();
                var organizationCategoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(organizationCategories);
                viewModel.Categories = organizationCategoriesVm;


                return View((viewModel.Id == default(long)) ? nameof(Create) : nameof(Edit), viewModel);
            }
        }
    }
}