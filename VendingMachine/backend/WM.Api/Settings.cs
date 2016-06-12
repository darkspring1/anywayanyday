
using System.Configuration;

namespace VM.Api
{
    public class Settings
    {
        public static string Url { get; private set; }
        public static string Port { get; private set; }

        public static string AppName { get; private set; }
        
        static Settings()
        {
            Url = ConfigurationManager.AppSettings["url"];
            Port = ConfigurationManager.AppSettings["port"];
            AppName = ConfigurationManager.AppSettings["appName"];
        }
    }
    
}
