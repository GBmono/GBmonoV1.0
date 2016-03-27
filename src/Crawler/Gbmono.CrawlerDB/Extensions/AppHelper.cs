using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GbmonoCrawlerDB.Extensions
{
    public class AppHelper
    {
        private static string _appconfig = null;

        public static string AppConfig
        {
            get
            {
                if (_appconfig == null)
                {
                    Type t = typeof(System.Configuration.ConfigurationManager).Assembly.GetType("System.Configuration.ClientConfigurationHost");
                    object cfghst = Activator.CreateInstance(t, true);
                    PropertyInfo pi = t.GetProperty("ConfigPaths", BindingFlags.Instance | BindingFlags.NonPublic);
                    object cfgpath = pi.GetValue(cfghst, null);

                    Type t1 = typeof(System.Configuration.ConfigurationManager).Assembly.GetType("System.Configuration.ClientConfigPaths");
                    pi = t1.GetProperty("ApplicationConfigUri", BindingFlags.Instance | BindingFlags.NonPublic);
                    string path = (string)pi.GetValue(cfgpath, null);

                    System.Console.WriteLine(path);
                    if (string.IsNullOrEmpty(path))
                        _appconfig = string.Empty;
                    else
                        _appconfig = path.Replace(".vshost.", ".");
                }
                return _appconfig;
            }
            set
            {
                _appconfig = value;
            }
        }

        public static void SetSettingToAppConfig(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("key not be null");
            }
            else
            {
                key = key.Trim();
            }
            if (string.IsNullOrEmpty(value))
                value = "";
            else
                value = value.Trim();

            if (!File.Exists(AppConfig))
            {
                throw new DirectoryNotFoundException();
            }
            File.SetAttributes(AppConfig, FileAttributes.Normal);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(AppConfig);
            XmlNodeList xmllst = xmldoc.SelectNodes("/configuration/appSettings/add");
            if (xmldoc.SelectSingleNode("/configuration/appSettings") == null)
            {
                XmlNode n2 = xmldoc.CreateNode("element", "appSettings", "");
                n2.InnerXml = "<add key=\"" + key + "\" value=\"" + value + "\"/>";
                xmldoc.SelectSingleNode("/configuration").AppendChild(n2);
                xmldoc.Save(AppConfig);
            }
            else if (xmllst.Count == 0)
            {
                XmlNode n2 = xmldoc.CreateNode("element", "add", "");
                XmlAttribute xa = xmldoc.CreateAttribute("key");
                xa.Value = key;
                n2.Attributes.Append(xa);
                xa = xmldoc.CreateAttribute("value");
                xa.Value = value;
                n2.Attributes.Append(xa);
                xmldoc.SelectSingleNode("/configuration/appSettings").AppendChild(n2);
                xmldoc.Save(AppConfig);
            }
            else
            {
                bool existed = false;
                foreach (XmlNode n1 in xmllst)
                {
                    if (n1.Attributes["key"].Value.ToUpper() == key.ToUpper())
                    {
                        n1.Attributes["value"].Value = value;
                        xmldoc.Save(AppConfig);
                        existed = true;
                        break;
                    }
                }
                if (!existed)
                {
                    XmlNode xmlnd = xmldoc.SelectSingleNode("/configuration/appSettings");
                    XmlNode n2 = xmldoc.CreateNode("element", "add", "");
                    XmlAttribute xa = xmldoc.CreateAttribute("key");
                    xa.Value = key;
                    n2.Attributes.Append(xa);
                    xa = xmldoc.CreateAttribute("value");
                    xa.Value = value;
                    n2.Attributes.Append(xa);
                    xmlnd.AppendChild(n2);
                    xmldoc.Save(AppConfig);
                }
            }
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
