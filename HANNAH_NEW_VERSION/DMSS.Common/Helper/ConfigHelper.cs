using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Helper
{
    public static class ConfigHelper
    {
        public static string GetConFig(string key)
        {
            var value = ConfigurationManager.AppSettings[key].ToString();
            return value;
        }
    }
}
