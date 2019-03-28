using System;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.OData.Client;
using NBlackout.Framework.Web.Mvc.Controllers;
using NBlackout.MoneyManager.Web.Extensions;
using NLog;

namespace NBlackout.MoneyManager.Web.Controllers
{
    public class BaseController : IdentitySecureController
    {
        #region Properties

        protected Logger Logger
        {
            get { return LogManager.GetCurrentClassLogger(); }
        }

        #endregion

        #region Controller members

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version.ToString();

            ViewBag.Version = version;

            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            Logger.Log(LogLevel.Warn, filterContext.Exception);

            var exception = filterContext.Exception.FindInnerException<DataServiceQueryException>();
            if (exception != null && exception.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }

        #endregion

        #region Methods

        protected void FlagBadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        #endregion
    }
}
