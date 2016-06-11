using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin;
using NLog;
using VM.Api.StructureMap;

namespace VM.Api
{
    
    class App
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static JsonSerializerSettings json;
        public void Configuration(IAppBuilder builder)
        {
            var configuration = new System.Web.Http.HttpConfiguration();

            configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(new VMRegistry());

            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            json = configuration.Formatters.JsonFormatter.SerializerSettings;

            configuration.Routes.MapHttpRoute("api", "api/{controller}/{action}");

            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var fsConfig = new Microsoft.Owin.StaticFiles.FileServerOptions
                {
                    EnableDirectoryBrowsing = true,
                    FileSystem = new PhysicalFileSystem(Settings.RootPath),
                    RequestPath = PathString.Empty,
                    EnableDefaultFiles = true
                    
                };

            fsConfig.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };

            builder
                .UseFileServer(fsConfig)
                .UseWebApi(configuration)
                
                .Use((c, next) =>
                {

                    var path = c.Request.Path.Value;
                    if (path.Contains(".") || path.Contains("api/"))
                    {
                        c.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Task.FromResult(0);
                    }
                    
                    return c.Response.SendFileAsync(Path.Combine(Settings.RootPath, "index.html"));   
                });
        }
    }
}
