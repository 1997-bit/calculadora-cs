using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace calculadora_web_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.MapHttpAttributeRoutes();
            // Rutas de API web - CON action como en Lab 19
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var formatter = config.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Remover XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}