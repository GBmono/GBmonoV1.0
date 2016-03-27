using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using GbmonoCrawlerDB.Extensions;
using NCrawler.Interfaces;
using NCrawler.Services;
using Newtonsoft.Json;
using Gbmono.CrawlerModel;

namespace Gbmono.CrawlerDB
{
    class Program
    {
        public static IFilter[] ExtensionsToSkip = new[]
			{
				(RegexFilter)new Regex(@"(\.jpg|\.css|\.js|\.gif|\.jpeg|\.png|\.ico|\.xml)",
					RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
			};

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            //CrawlUsingDbStorage.Run();

            log4net.ILog log = log4net.LogManager.GetLogger("logger-name");

            //Old Working
            //if (args.Any())
            //{
            //    AppHelper.SetSettingToAppConfig("CrawlTargetUrl", args.First());
            //    var originalWebSitePaht = AppDomain.CurrentDomain.BaseDirectory + "OriginalWebSite.txt";
            //    File.WriteAllText(originalWebSitePaht, args.First());
            //}z
            //else
            //{
            //    CrawlUsingDbStorage.Run();
            //}

            if (args.Any())
            {
                AppHelper.SetSettingToAppConfig("CrawlTargetUrl", args.First());
                var originalWebSitePaht = AppDomain.CurrentDomain.BaseDirectory + "OriginalWebSite.txt";
                try
                {
                    var recipeRegex = args.Length >= 3 && args[2] != "empty" ? args[2].Split(';').ToList() : null;
                    var storeRegex = args.Length >= 4 && args[3] != "empty" ? args[3].Split(';').ToList() : null;
                    var blockRegex = args.Length >= 5 && args[4] != "empty" ? args[4].Split(';').ToList() : null;
                    var crawlDepth = args.Length >= 6 ? args[5] : "0";
                    var depth = 0;
                    int.TryParse(crawlDepth, out depth);
                    if (depth != 0)
                    {
                        AppHelper.SetSettingToAppConfig("MaximumCrawlDepth", depth.ToString());
                    }
                    File.WriteAllText(originalWebSitePaht, JsonConvert.SerializeObject(new OriginalWebSiteTxt { OriginalWebSite = args.First(), ThridPartyUserId = args[1], StoreRegex = storeRegex, RecipeRegex = recipeRegex, BlockRegex = blockRegex }));
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

            }
            else
            {
                try
                {
                    CrawlUsingDbStorage.Run();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }


        }
    }
}
