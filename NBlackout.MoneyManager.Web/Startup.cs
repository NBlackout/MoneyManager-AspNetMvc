using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NBlackout.MoneyManager.Web.OData;
using Owin;

namespace NBlackout.MoneyManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();

            ConfigureOAuth(app);
            
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            ODataConfig.Configure(config);
            app.UseWebApi(config);
        }
    }
}
