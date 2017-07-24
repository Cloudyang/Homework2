using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Common
{
    public class EnvironmentArgument
    {
        public static string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogPath"]);

        public static string XmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["XmlPath"]);

        public static string JsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["JsonPath"]);

        static EnvironmentArgument()
        {
            Directory.CreateDirectory(LogPath);
            Directory.CreateDirectory(XmlPath);
            Directory.CreateDirectory(JsonPath);

        }
    }
}
