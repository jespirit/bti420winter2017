using System.Web.Http;

namespace Assignment4
{
    public static class WebApiConfig
    {
        public static void RegisterApi(HttpConfiguration config)
        {
            BaseWebApiConfig.RegisterConfiguration(config);

            config.Routes.MapHttpRoute(
                name: "Invoice",
                routeTemplate: "api/invoice/{action}",
                defaults: new { controller = "InvoiceApi" }
            );
        }
    }
}
