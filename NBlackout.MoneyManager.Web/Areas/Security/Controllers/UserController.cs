using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using NBlackout.MoneyManager.Resources;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;

namespace NBlackout.MoneyManager.Web.Areas.Security.Controllers
{
    public class UserController : BaseController
    {
        public async Task<ActionResult> UserInfo()
        {
            var user = await CurrentUserAsync();
            var userVm = AutoMapperConfig.Mapper.Map<UserViewModel>(user);

            return Json(userVm, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ManageAccount()
        {
            var user = await CurrentUserAsync();
            var userVm = AutoMapperConfig.Mapper.Map<UserViewModel>(user);

            return View(userVm);
        }

        public ActionResult EditPassword()
        {
            var viewModel = new UpdatePasswordViewModel();

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SavePassword(UpdatePasswordViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            try
            {
                if (ModelState.IsValid)
                {
                    if (String.Equals(viewModel.Password, viewModel.PasswordConfirmation))
                    {
                        var result = await ChangePasswordAsync(viewModel.OldPassword, viewModel.Password);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(nameof(viewModel.OldPassword), String.Join(",", result.Errors));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(viewModel.PasswordConfirmation), Resource.Messages_PasswordNotMatchingConfirmation);
                    }
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            return View(nameof(EditPassword));
        }

        public ActionResult EditEmail()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveEmail(UpdateEmailViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            try
            {
                if (ModelState.IsValid)
                {
                    if (String.Equals(viewModel.Email, viewModel.EmailConfirmation))
                    {
                        var url = Url.Action(nameof(VerifyEmail), "User", new { UserId = "__UserId__", Token = "__Token__" }, Request.Url.Scheme);

                        await ChangeEmailAsync(viewModel.Email, url);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(viewModel.EmailConfirmation), Resource.Messages_EmailNotMatchingConfirmation);
                    }
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            return View(nameof(EditEmail), viewModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyEmail(VerifyEmailViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await ConfirmEmailAsync(viewModel.UserId, viewModel.Token);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError(nameof(viewModel.Token), Resource.Messages_TokenInvalid);
                    }
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            return View(nameof(VerifyEmail), viewModel);
        }

        public ActionResult EditPhoneNumber()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SavePhoneNumber(UpdatePhoneNumberViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            try
            {
                if (ModelState.IsValid)
                {
                    if (String.Equals(viewModel.PhoneNumber, viewModel.PhoneNumberConfirmation))
                    {
                        await ChangePhoneNumberAsync(viewModel.PhoneNumber);

                        return View(nameof(VerifyPhoneNumber), new VerifyPhoneNumberViewModel { PhoneNumber = viewModel.PhoneNumber });
                    }

                    ModelState.AddModelError(nameof(viewModel.PhoneNumberConfirmation), Resource.Messages_PhoneNumberNotMatchingConfirmation);
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            return View(nameof(EditPhoneNumber), viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await ConfirmPhoneNumberAsync(viewModel.PhoneNumber, viewModel.Code);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError(nameof(viewModel.Code), Resource.Messages_CodeInvalid);
                    }
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            return View(nameof(VerifyPhoneNumber), viewModel);
        }
    }
}