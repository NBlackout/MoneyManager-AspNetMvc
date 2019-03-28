using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Dal.Customer;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Administration.Controllers
{
    public class CustomersController : BaseController
    {
        private CustomerDao customerDao = new CustomerDao();

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
                var customers = await context.Customers.ToListAsync();
                var customersVm = AutoMapperConfig.Mapper.Map<IEnumerable<CustomerViewModel>>(customers);

                return PartialView("_List", customersVm);
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
                var customer = await context.Customers.FindAsync(id);
                var customerVm = AutoMapperConfig.Mapper.Map<CustomerViewModel>(customer);

                return View(customerVm);
            }
        }

        public ActionResult Delete(long id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var customers = context.Customers.Where(t => t.Id != id);
                var customersVm = AutoMapperConfig.Mapper.Map<IEnumerable<CustomerViewModel>>(customers);
                var viewModel = new ReassignCustomerViewModel { SourceCustomerId = id, Customers = customersVm };

                return View(viewModel);
            }
        }

        public async Task<ActionResult> ConfirmDelete(ReassignCustomerViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                var customers = context.Customers.Where(t => t.Id != viewModel.SourceCustomerId);
                var customersVm = AutoMapperConfig.Mapper.Map<IEnumerable<CustomerViewModel>>(customers);

                if (ModelState.IsValid)
                {
                    await customerDao.ReassignAccountsCustomer(viewModel.SourceCustomerId, viewModel.TargetCustomerId);

                    var customer = await context.Customers.FindAsync(viewModel.SourceCustomerId);
                    context.Customers.Remove(customer);

                    await context.SaveChangesAsync();
                }
                else
                {
                    FlagBadRequest();
                }

                return View(nameof(Delete), new ReassignCustomerViewModel { SourceCustomerId = viewModel.SourceCustomerId, Customers = customersVm });
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(CustomerViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!Request.IsAjaxRequest())
                return RedirectToAction(nameof(List));

            using (var context = new MoneyManagerDbContext())
            {
                if (ModelState.IsValid)
                {
                    var customer = AutoMapperConfig.Mapper.Map<CustomerModel>(viewModel);

                    if (viewModel.Id == default(long))
                        context.Customers.Add(customer);
                    else
                        context.Entry(customer).State = EntityState.Modified;

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