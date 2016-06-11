using System;
using Microsoft.Owin.Hosting;


namespace VM.Api
{
    public class Service
    {


        IDisposable _app;
    
        public void Start() {
            _app = WebApp.Start<App>(Settings.HostUrl);
        }
        public void Stop() { _app.Dispose(); }
    }
}
