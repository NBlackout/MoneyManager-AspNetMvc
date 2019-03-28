using System;
using System.Web.Mvc;

namespace NBlackout.MoneyManager.Web.Areas.Security
{
    public class SecurityAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Security"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.MapRoute(
                "Security",
                "Security/{controller}/{action}",
                new { controller = "Authentication", action = "Index" },
                new string[] { "NBlackout.MoneyManager.Web.Areas.Security.Controllers" }
            );
        }
    }
}