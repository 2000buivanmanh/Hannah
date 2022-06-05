using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HANNAH_NEW_VERSION
{
    public static class AppConfiguration
    {
        public static string ImagesPath => GetValue("ImagesPath");
        public static string AvatarPath => GetValue("AvatarPath");

        private static string GetValue(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            return value;
        }
    }
}