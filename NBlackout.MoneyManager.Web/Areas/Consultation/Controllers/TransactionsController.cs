using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Dal.Account;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Resources;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Consultation.Controllers
{
    public class TransactionsController : BaseController
    {
        private AccountDao accountDao = new AccountDao();

        public async Task<ActionResult> Index()
        {
            using (var context = new MoneyManagerDbContext())
            {
                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();
                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                var viewModel = new SearchTransactionsViewModel { Accounts = accountsVm, Organizations = organizationsVm };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> ListPartial(SearchTransactionsViewModel viewModel)
        {
            if (!Request.IsAjaxRequest())
                return Redirect(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    if (!viewModel.OrganizationId.HasValue)
                    {
                        if (String.IsNullOrWhiteSpace(viewModel.Label))
                            ModelState.AddModelError("ValidationError", Resource.Messages_OrganizationOrLabelRequired);
                        else if (viewModel.Label.Length < 3)
                            ModelState.AddModelError(nameof(viewModel.Label), Resource.Messages_LabelLength);
                    }
                }

                if (ModelState.IsValid)
                {
                    var transactions = context.Transactions.Include(m => m.Organization.Category).AsQueryable();
                    if (viewModel.AccountId.HasValue)
                        transactions = transactions.Where(t => t.AccountId == viewModel.AccountId.Value);
                    if (viewModel.OrganizationId.HasValue)
                        transactions = transactions.Where(t => t.OrganizationId == viewModel.OrganizationId.Value);
                    if (!String.IsNullOrWhiteSpace(viewModel.Label))
                        transactions = transactions.Where(t => t.Label.Contains(viewModel.Label) || (t.Description != null && t.Description.Contains(viewModel.Label)));

                    var transactionsVm = AutoMapperConfig.Mapper.Map<IEnumerable<TransactionViewModel>>(transactions);

                    return PartialView("_List", transactionsVm);
                }

                FlagBadRequest();

                var accounts = await context.Accounts.Include(m => m.Category).ToListAsync();
                var accountsVm = AutoMapperConfig.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

                var organizations = await context.Organizations.Include(m => m.Category).ToListAsync();
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                viewModel.Accounts = accountsVm;
                viewModel.Organizations = organizationsVm;

                return View(nameof(Index), viewModel);
            }
        }

        public async Task<ActionResult> TransactionsOfMonthPartial(long accountId, int year, int month)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var query = await accountDao.TransactionsOfMonthAsync(accountId, year, month);
                var transactions = await query.Include(m => m.Organization.Category).ToListAsync();
                var transactionsVm = AutoMapperConfig.Mapper.Map<IEnumerable<TransactionViewModel>>(transactions);

                return PartialView("_List", transactionsVm);
            }
        }

        public async Task<ActionResult> TransactionsOfImportHistory(long importHistoryId)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(Index));

            using (var context = new MoneyManagerDbContext())
            {
                var transactions = await context.Transactions.Where(m => m.ImportHistoryId == importHistoryId).ToListAsync();
                var transactionsVm = AutoMapperConfig.Mapper.Map<IEnumerable<TransactionViewModel>>(transactions);

                return PartialView("_List", transactionsVm);
            }
        }

        public async Task<ActionResult> Edit(long id)
        {
            using (var context = new MoneyManagerDbContext())
            {
                var organizations = await context.Organizations.Include(t => t.Category).ToListAsync();
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                var transaction = await context.Transactions.FindAsync(id);
                var transactionVm = AutoMapperConfig.Mapper.Map<TransactionViewModel>(transaction);
                transactionVm.Organizations = organizationsVm;

                return View(transactionVm);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(TransactionViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            using (var context = new MoneyManagerDbContext())
            {
                var organizations = await context.Organizations.Include(t => t.Category).ToListAsync();
                var organizationsVm = AutoMapperConfig.Mapper.Map<IEnumerable<OrganizationViewModel>>(organizations);

                if (ModelState.IsValid)
                {
                    var transaction = AutoMapperConfig.Mapper.Map<TransactionModel>(viewModel);

                    context.Entry(transaction).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                viewModel.Organizations = organizationsVm;

                return View(nameof(Edit), viewModel);
            }
        }

        public async Task<ActionResult> UpdateOrganizations(UpdateOrganizationViewModel[] viewModels)
        {
            if (viewModels == null)
                throw new ArgumentNullException(nameof(viewModels));

            foreach (var viewModel in viewModels)
                await UpdateOrganization(viewModel);

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> UpdateOrganization(UpdateOrganizationViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            using (var context = new MoneyManagerDbContext())
            {
                var transaction = await context.Transactions.FindAsync(viewModel.TransactionId);
                transaction.OrganizationId = viewModel.OrganizationId;

                context.Entry(transaction).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}