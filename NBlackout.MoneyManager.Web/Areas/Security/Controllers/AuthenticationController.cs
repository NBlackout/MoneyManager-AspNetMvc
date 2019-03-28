using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using NBlackout.MoneyManager.Resources;
using NBlackout.MoneyManager.ViewModels;
using NBlackout.MoneyManager.Web.Controllers;
using NLog;

namespace NBlackout.MoneyManager.Web.Areas.Security.Controllers
{
    public class AuthenticationController : BaseController
    {
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            var viewModel = new AuthenticationViewModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(AuthenticationViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (ModelState.IsValid)
            {
                try
                {
                    var returnUrl = viewModel.ReturnUrl;

                    var result = await PasswordSignInAsync(viewModel.UserName, viewModel.Password);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);

                            return RedirectToAction("Index", "Dashboard", new { area = "Consultation" });
                        case SignInStatus.RequiresVerification:
                            Logger.Log(LogLevel.Info, String.Format(Resource.Messages_UserRequiresTfaVerification, viewModel.UserName));
                            Session["UserName"] = viewModel.UserName;
                            Session["Password"] = viewModel.Password;

                            return RedirectToAction(nameof(SelectTokenProvider), new { ReturnUrl = returnUrl });
                        default:
                            ModelState.AddModelError("Login", Resource.Messages_UserNameOrPasswordInvalid);

                            break;
                    }
                }
                catch (WebException e)
                {
                    var response = e.Response as HttpWebResponse;
                    if (response == null)
                        throw;

                    if (response.StatusCode != HttpStatusCode.BadRequest)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            var content = reader.ReadToEnd();

                            throw new WebException(content);
                        }
                    }

                    ModelState.AddModelError("Login", Resource.Messages_UserNameOrPasswordInvalid);
                }
            }

            return View(nameof(SignIn), viewModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> SelectTokenProvider(string returnUrl)
        {
            if (Session == null || String.IsNullOrEmpty(Session["UserName"] as string))
                return RedirectToAction(nameof(SignIn), (object)returnUrl);

            var tokenProviders = await TwoFactorProvidersAsync();
            var viewModel = new SelectTokenProviderViewModel { ReturnUrl = returnUrl };

            if (tokenProviders.Count() == 1)
            {
                var tokenProvider = tokenProviders.Single();
                viewModel.TokenProvider = tokenProvider;

                return RedirectToAction(nameof(SendCode), viewModel);
            }

            viewModel.TokenProviders = tokenProviders;

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(SelectTokenProviderViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (Session == null || String.IsNullOrEmpty(Session["UserName"] as string))
                return RedirectToAction(nameof(SignIn), (object)viewModel.ReturnUrl);

            try
            {
                if (ModelState.IsValid)
                {
                    Logger.Log(LogLevel.Info, String.Format(Resource.Messages_SendingTwoFactorCode, viewModel.TokenProvider, Session["UserName"].ToString()));
                    var result = await SendTwoFactorCodeAsync(viewModel.TokenProvider);
                    if (result)
                    {
                        var sendCodeViewModel = new SendCodeViewModel { TokenProvider = viewModel.TokenProvider, ReturnUrl = viewModel.ReturnUrl };

                        return View(nameof(SendCode), sendCodeViewModel);
                    }

                    ModelState.AddModelError(nameof(viewModel.TokenProvider), Resource.Messages_ProviderUnavailable);
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            viewModel.TokenProviders = await TwoFactorProvidersAsync();

            return View(nameof(SelectTokenProvider), viewModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(SendCodeViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (Session == null || String.IsNullOrEmpty(Session["UserName"] as string))
                return RedirectToAction(nameof(SignIn), (object)viewModel.ReturnUrl);

            try
            {
                if (ModelState.IsValid)
                {
                    var returnUrl = viewModel.ReturnUrl;

                    var result = await TwoFactorSignInAsync(viewModel.TokenProvider, viewModel.Code);
                    if (result == SignInStatus.Success)
                    {
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return RedirectToAction("Index", "Dashboard", new { area = "Consultation" });
                    }

                    ModelState.AddModelError(nameof(viewModel.Code), Resource.Messages_CodeInvalid);
                }
            }
            finally
            {
                if (!ModelState.IsValid)
                    FlagBadRequest();
            }

            return View(nameof(SendCode), viewModel);
        }

        public async Task<ActionResult> SignOut()
        {
            await SignOutAsync();

            return RedirectToAction("Index", "Dashboard", new { area = "Consultation" });
        }
    }
}