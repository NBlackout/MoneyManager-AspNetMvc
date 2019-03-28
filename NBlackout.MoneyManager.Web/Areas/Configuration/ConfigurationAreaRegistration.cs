using System;
using System.Web.Mvc;

namespace NBlackout.MoneyManager.Web.Areas.Configuration
{
    public class ConfigurationAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Configuration"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.MapRoute(
                "Configuration",
                "Configuration/{controller}/{action}",
                new { controller = "DataManagement", action = "Index" },
                new string[] { "NBlackout.MoneyManager.Web.Areas.Configuration.Controllers" }
            );
        }
    }
}