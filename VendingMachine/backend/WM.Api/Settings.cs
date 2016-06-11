using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Api
{
    class Settings
    {
        public static string RootPath { get { return ConfigurationManager.AppSettings["rootPath"]; }  }
        public static string HostUrl { get { return ConfigurationManager.AppSettings["hostUrl"]; } }
    }
}
