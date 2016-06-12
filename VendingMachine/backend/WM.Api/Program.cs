﻿using NLog;
using System;
using Topshelf;

namespace VM.Api
{
    class Program
    {

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {
            try
            {
                var serviceName = Settings.AppName;
                //берём из конфига
                string port = Settings.Port;
                HostFactory.Run(x =>
                {
                    //переопределяем порт параметром из командно строки, если он есть
                    x.AddCommandLineDefinition("port", f => { port = f; });
                    x.ApplyCommandLine();
                    x.UseNLog();
                    _logger.Info("Lisen {0}:{1}", Settings.Url, port);
                    x.Service<Service>(s =>
                    {
                        s.ConstructUsing(name => new Service());
                        s.WhenStarted(tc => tc.Start(Settings.Url, int.Parse(port)));
                        s.WhenStopped(tc => tc.Stop());

                    });

                    x.RunAsLocalSystem();

                    x.SetDescription(serviceName);
                    x.SetDisplayName(serviceName);
                    x.SetServiceName(serviceName);
                });
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
    }
}
