using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using NLog;

namespace VM.Api.Middlewares
{
    public class NLogMiddleware : OwinMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public NLogMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            Logger.Info("START REQUEST {0}", context.Request.Uri);
            Logger.Info("REQUEST {0} Host: {1}", context.Request.Uri, context.Request.Host);
            Logger.Info("REQUEST {0} LocalIpAddress:  {1}", context.Request.Uri, context.Request.LocalIpAddress);
            Logger.Info("REQUEST {0} LocalPort: {1}", context.Request.Uri, context.Request.LocalPort);
            Logger.Info("REQUEST {0} RemoteIpAddress: {1}", context.Request.Uri, context.Request.RemoteIpAddress);
            Logger.Info("REQUEST {0} RemotePort: {1}", context.Request.Uri, context.Request.RemotePort);
            Logger.Info("REQUEST {0} Method: {1}", context.Request.Uri, context.Request.Method);
            Logger.Info("REQUEST {0} QueryString: {1}", context.Request.Uri, context.Request.QueryString);
            //_logger.Info("REQUEST {0} Length: {1}", context.Request.Uri, context.Request.Body.Length);
            Logger.Info("REQUEST {0} Headers: {1}", context.Request.Uri, JsonConvert.SerializeObject(context.Request.Headers));
            return Next.Invoke(context);
        }
    }
}