using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace VM.Api
{
    

    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {
            try
            {
                HostFactory.Run(x =>
                {
                    x.UseNLog();
                    x.Service<Service>(s =>
                    {
                        s.ConstructUsing(name => new Service());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());

                    });

                    x.RunAsLocalSystem();

                    x.SetDescription("VM Topshelf Host");
                    x.SetDisplayName("VM");
                    x.SetServiceName("VM");
                });
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }
    }
}
