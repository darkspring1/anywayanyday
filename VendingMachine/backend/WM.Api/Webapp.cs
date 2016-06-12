using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using NLog;
using Owin;
using StructureMap;
using VM.Api.Ioc;
using VM.Api.Middlewares;


namespace VM.Api
{

    class App
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Container Container = new Container(new VmRegistry());

        public void Configuration(IAppBuilder app)
        {
            Logger.Debug("Start configuration");

            var config = new HttpConfiguration();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
            config.Services.Replace(typeof(IHttpControllerActivator), new StructureMapWebApiControllerActivator(Container));

            //configuration.Routes.MapHttpRoute("api", "api/{controller}/{action}");
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            app
                .UseLogger()
                .UseWebApi(config)
                .Use((c, next) =>
                {
                    c.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Task.FromResult(0);
                });


            Logger.Debug("Configuration finished");
        }
    }
}
