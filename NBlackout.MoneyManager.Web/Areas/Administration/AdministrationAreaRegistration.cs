using System;
using System.Web.Mvc;

namespace NBlackout.MoneyManager.Web.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Administration"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.MapRoute(
                "Administration",
                "Administration/{controller}/{action}",
                new { controller = "DataManagement", action = "Index" },
                new string[] { "NBlackout.MoneyManager.Web.Areas.Administration.Controllers" }
            );
        }
    }
}