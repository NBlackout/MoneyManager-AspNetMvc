using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Dal.Organization;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Configuration.Controllers
{
    public class OrganizationsController : BaseController
    {
        private OrganizationDao organizationDao = new OrganizationDao();

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
                var organizations = await context.Organizations.Include(t => t.Category.Category).Include(t => t.AutomationElements.Select(m => m.Account.Customer)).Where(t => t.Label != "All").ToListAsync();
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                return PartialView("_List", organizationsVm);
            }
        }

        public async Task<ActionResult> Create()
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var categories = await context.OrganizationCategories.Include(t => t.Category).ToListAsync();
                var categoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(categories);

                var viewModel = new OrganizationViewModel { Categories = categoriesVm };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> Edit(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var organization = await context.Organizations.Include(t => t.Category).SingleOrDefaultAsync(t => t.Id == id);
                var categories = await context.OrganizationCategories.Include(t => t.Category).ToListAsync();

                var categoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(categories);
                var organizationVm = AutoMapperConfig.Mapper.Map<OrganizationViewModel>(organization);

                organizationVm.Categories = categoriesVm;

                return View(organizationVm);
            }
        }

        public ActionResult Delete(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var organizations = context.Organizations.Where(t => t.Id != id);
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                var viewModel = new ReassignOrganizationViewModel { SourceOrganizationId = id, Organizations= organizationsVm };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> ConfirmDelete(ReassignOrganizationViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var organizations = context.Organizations.Where(t => t.Id != viewModel.SourceOrganizationId);
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                if (ModelState.IsValid)
                {
                    await organizationDao.ReassignTransactionsOrganization(viewModel.SourceOrganizationId, viewModel.TargetOrganizationId);

                    var organization = await context.Organizations.FindAsync(viewModel.SourceOrganizationId);
                    context.Entry(organization).State = EntityState.Deleted;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                return View(nameof(Delete), new ReassignOrganizationViewModel { SourceOrganizationId = viewModel.SourceOrganizationId, Organizations = organizationsVm });
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(OrganizationViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var categories = await context.OrganizationCategories.Include(t => t.Category).ToListAsync();
                var categoriesVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationCategoryViewModel>>(categories);

                if (ModelState.IsValid)
                {
                    var organization = AutoMapperConfig.Mapper.Map<OrganizationModel>(viewModel);

                    if (viewModel.Id == default(long))
                        context.Organizations.Add(organization);
                    else
                        context.Entry(organization).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                viewModel.Categories = categoriesVm;

                return View((viewModel.Id == default(long)) ? nameof(Create) : nameof(Edit), viewModel);
            }
        }

        #region AutomationElement

        public async Task<ActionResult> CreateAutomationElement(long organizationId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var organization = await context.Organizations.FindAsync(organizationId);
                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();

                var organizationVm = AutoMapperConfig.Mapper.Map<OrganizationViewModel>(organization);
                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

                var automationElementVm = new AutomationElementViewModel
                {
                    OrganizationId = organizationId,
                    Organization = organizationVm,
                    Accounts = accountsVm
                };

                return View(automationElementVm);
            }
        }

        public async Task<ActionResult> EditAutomationElement(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var automationElement = await context.AutomationElements.Include(t => t.Tags).SingleOrDefaultAsync(t => t.Id == id);
                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();
                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();

                var organizationsVm = AutoMapperConfig.Mapper.Map<ICollection<OrganizationViewModel>>(organizations);
                var accountsVm = AutoMapperConfig.Mapper.Map<ICollection<AccountViewModel>>(accounts);

                var automationElementVm = AutoMapperConfig.Mapper.Map<AutomationElementViewModel>(automationElement);
                automationElementVm.Organizations = organizationsVm;
                automationElementVm.Accounts = accountsVm;

                return View(automationElementVm);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveAutomationElement(AutomationElementViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();
                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();

                var organizationsVm = AutoMapperConfig.Mapper.Map<ICollection<OrganizationViewModel>>(organizations);
                var accountsVm = AutoMapperConfig.Mapper.Map<ICollection<AccountViewModel>>(accounts);

                if (ModelState.IsValid)
                {
                    var automationElement = AutoMapperConfig.Mapper.Map<AutomationElementModel>(viewModel);
                    if (automationElement.Id == default(long))
                    {
                        context.AutomationElements.Add(automationElement);
                    }
                    else
                    {
                        foreach (var tag in automationElement.Tags)
                        {
                            if (tag.Id == default(long))
                                context.AutomationElementTags.Add(tag);
                            else
                                context.AutomationElementTags.Attach(tag);
                        }

                        context.Entry(automationElement).State = EntityState.Modified;
                    }

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                viewModel.Organizations = organizationsVm;
                viewModel.Accounts = accountsVm;

                return View((viewModel.Id == default(long)) ? nameof(CreateAutomationElement) : nameof(EditAutomationElement), viewModel);
            }
        }

        public async Task<ActionResult> DeleteAutomationElement(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var model = await context.AutomationElements.FindAsync(id);
                var viewModel = AutoMapperConfig.Mapper.Map<AutomationElementViewModel>(model);

                return View(viewModel);
            }
        }

        public async Task<ActionResult> ConfirmDeleteAutomationElement(AutomationElementViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    var automationElement = await context.AutomationElements.FindAsync(viewModel.Id);
                    context.Entry(automationElement).State = EntityState.Deleted;

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                return View(nameof(DeleteAutomationElement), viewModel);
            }
        }

        #endregion
    }
}