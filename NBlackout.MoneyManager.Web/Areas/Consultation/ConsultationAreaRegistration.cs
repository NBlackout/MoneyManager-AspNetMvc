using System;
using System.Web.Mvc;

namespace NBlackout.MoneyManager.Web.Areas.Consultation
{
    public class ConsultationAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Consultation"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.MapRoute(
                "AccountActivity",
                "Consultation/Accounts/Activity/{id}",
                new { controller = "Accounts", action = "ActivityPartial" },
                new { id = @"\d+" },
                new string[] { "NBlackout.MoneyManager.Web.Areas.Consultation.Controllers" }

            );
            context.MapRoute(
                "Consultation",
                "Consultation/{controller}/{action}",
                new { controller = "Dashboard", action = "Index" },
                new string[] { "NBlackout.MoneyManager.Web.Areas.Consultation.Controllers" }
            );

            context.Routes.MapMvcAttributeRoutes();
        }
    }
}