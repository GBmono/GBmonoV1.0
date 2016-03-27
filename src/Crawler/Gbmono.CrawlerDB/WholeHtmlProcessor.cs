using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Gbmono.CrawlerDB.Extensions;
using Gbmono.CrawlerDB.Properties;
using HtmlAgilityPack;
using NCrawler;
using NCrawler.Extensions;
using NCrawler.HtmlProcessor.Extensions;

using NCrawler.Interfaces;
using NCrawler.Utils;
using Newtonsoft.Json;
using RestSharp;
using Gbmono.CrawlerModel;
using System.Runtime.Caching;
using System.Web;

namespace Gbmono.CrawlerDB
{
    public class WholeHtmlProcessor : ContentCrawlerRules, IPipelineStep
    {
        #region Constructors


        public WholeHtmlProcessor()
            : this(null, null)
        {
        }

        public WholeHtmlProcessor(Dictionary<string, string> filterTextRules,
            Dictionary<string, string> filterLinksRules)
            : base(filterTextRules, filterLinksRules)
        {
        }

        private ObjectCache cache
        {
            get { return MemoryCache.Default; }
        }

        #endregion

        #region Instance Methods

        protected virtual string NormalizeLink(string baseUrl, string link)
        {
            return link.NormalizeUri(baseUrl);
        }

        #endregion

        #region IPipelineStep Members

        public void Process(Crawler crawler, PropertyBag propertyBag)
        {


            AspectF.Define.
                NotNull(crawler, "crawler").
                NotNull(propertyBag, "propertyBag");

            string stepUri = Uri.UnescapeDataString(propertyBag.Step.Uri.AbsoluteUri);
            if (stepUri.Length > 396)
            {
                stepUri = stepUri.Substring(0, 396);
            }
            var crawlHistory = AspectF.Define.
               Return<CrawlHistory, NCrawlerEntitiesDbServices>(
                   e => e.CrawlHistory.Where(m => m.Key == stepUri).FirstOrDefault());

            if (crawlHistory == null)
            {
                AspectF.Define.Do<NCrawlerEntitiesDbServices>(e =>
                {
                    e.ExecuteStoreCommand("delete Crawlqueue where [key] ={0}", stepUri);
                });
                return;
            }
            try
            {
                if (propertyBag.StatusCode != HttpStatusCode.OK)
                {
                    AspectF.Define.Do<NCrawlerEntitiesDbServices>(e =>
                    {
                        e.ExecuteStoreCommand("delete Crawlqueue where [key] ={0}", crawlHistory.Key);
                        //CrawlQueue result = e.CrawlQueue.FirstOrDefault(q => q.Key == crawlHistory.Key);
                        //if (!result.IsNull())
                        //{
                        //    e.DeleteObject(result);
                        //    e.SaveChanges();
                        //}
                    });
                    return;
                }

                if (!IsHtmlContent(propertyBag.ContentType))
                {
                    AspectF.Define.Do<NCrawlerEntitiesDbServices>(e =>
                    {
                        e.ExecuteStoreCommand("delete Crawlqueue where [key] ={0}", crawlHistory.Key);
                        //CrawlQueue result = e.CrawlQueue.FirstOrDefault(q => q.Key == crawlHistory.Key);
                        //if (!result.IsNull())
                        //{
                        //    e.DeleteObject(result);
                        //    e.SaveChanges();
                        //}
                    });
                    return;
                }
                HtmlDocument htmlDoc = new HtmlDocument
                {
                    OptionAddDebuggingAttributes = false,
                    OptionAutoCloseOnEnd = true,
                    OptionFixNestedTags = true,
                    OptionReadEncoding = true
                };
                using (Stream reader = propertyBag.GetResponse())
                {
                    Encoding documentEncoding = htmlDoc.DetectEncoding(reader);
                    reader.Seek(0, SeekOrigin.Begin);
                    if (!documentEncoding.IsNull())
                    {
                        htmlDoc.Load(reader, documentEncoding, true);
                    }
                    else
                    {
                        htmlDoc.Load(reader, true);
                    }

                    //string content = reader.ReadToEnd();
                    //resultHtmlContent = content;
                }
                //string steplUri = propertyBag.ResponseUri.OriginalString;


                string orginalHtmlContent = htmlDoc.DocumentNode.OuterHtml;
                string baseUrl = propertyBag.ResponseUri.GetLeftPart(UriPartial.Path);
                DocumentWithLinks links = htmlDoc.GetLinks();



                //string urlRegex = @"^http://www.bbc.co.uk/food/recipes/[^#/]+$";
                List<string> recipeRegex = null;
                var jsonStr = cache.Get(AppDomain.CurrentDomain.BaseDirectory + "OriginalWebSite") as string;
                if (jsonStr == null)
                {
                    using (var stream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "OriginalWebSite.txt", Encoding.UTF8))
                    {
                        jsonStr = stream.ReadToEnd();
                        var policy = new CacheItemPolicy();
                        policy.Priority = CacheItemPriority.NotRemovable;
                        policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
                        cache.Set(AppDomain.CurrentDomain.BaseDirectory + "OriginalWebSite", jsonStr, policy);
                        Console.WriteLine("cache --" + AppDomain.CurrentDomain.BaseDirectory + " :" + cache.Get(AppDomain.CurrentDomain.BaseDirectory + "OriginalWebSite"));
                    }
                }
                var json = JsonConvert.DeserializeObject<OriginalWebSiteTxt>(jsonStr);
                if (json.RecipeRegex != null && json.RecipeRegex.Count > 0)
                {
                    recipeRegex = json.RecipeRegex;
                }
                bool needToStore = false;

                if (recipeRegex != null)
                {
                    foreach (var regex in recipeRegex)
                    {
                        if (Regex.IsMatch(propertyBag.Step.Uri.AbsoluteUri, regex, RegexOptions.IgnoreCase))
                        {
                            needToStore = true;
                            break;
                        }
                    }
                }
                else
                {
                    needToStore = true;
                }

                if (needToStore)
                {
                    //string folderPath = "D:/CrawlerManager/CrawlerData";
                    //string instanceFolderPath = folderPath + "/" + crawlHistory.GroupId;
                    //string path = folderPath + "/" + crawlHistory.GroupId + "/" + string.Format("{0}.txt", crawlHistory.Id);
                    //if (!Directory.Exists(folderPath))
                    //{
                    //    Directory.CreateDirectory(folderPath);
                    //}
                    //if (!Directory.Exists(instanceFolderPath))
                    //{
                    //    Directory.CreateDirectory(instanceFolderPath);
                    //}

                    //if (!File.Exists(path))
                    //{
                    //    try
                    //    {

                    //        using (StreamWriter sw = File.CreateText(path))
                    //        {
                    //            sw.WriteLine(orginalHtmlContent);
                    //        }

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        log4net.Config.XmlConfigurator.Configure();
                    //        log4net.ILog log = log4net.LogManager.GetLogger("logger-name");
                    //        log.Error(ex);
                    //    }
                    //}
                    var folderHelper = new FolderHelper();
                    var path = folderHelper.GetFolderPathToStore(crawlHistory.GroupId) + "/" + string.Format("{0}.txt", crawlHistory.Id);
                    Console.Write(path);

                    if (!File.Exists(path))
                    {
                        try
                        {
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(orginalHtmlContent);
                            }

                        }
                        catch (Exception ex)
                        {
                            log4net.Config.XmlConfigurator.Configure();
                            log4net.ILog log = log4net.LogManager.GetLogger("logger-name");
                            log.Error(ex);
                        }
                    }
                    //}
                }



                AspectF.Define.Do<NCrawlerEntitiesDbServices>(e =>
                {
                    e.ExecuteStoreCommand("delete Crawlqueue where [key] ={0}", crawlHistory.Key);
                });

                foreach (string link in links.Links.Union(links.References))
                {
                    if (link.IsNullOrEmpty() || link.Length > 396)
                    {
                        continue;
                    }

                    string decodedLink = ExtendedHtmlUtility.HtmlEntityDecode(link);
                    string normalizedLink = "";
                    try
                    {
                        normalizedLink = NormalizeLink(baseUrl, decodedLink);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    
                    if (normalizedLink.IsNullOrEmpty())
                    {
                        continue;
                    }
                    if (link.Contains("page="))
                    {
                        var a = 1;
                    }


                    crawler.AddStep(new Uri(normalizedLink), propertyBag.Step.Depth + 1,
                        propertyBag.Step, new Dictionary<string, object>
                        {
                            {Resources.PropertyBagKeyOriginalUrl, link},
                            {Resources.PropertyBagKeyOriginalReferrerUrl, propertyBag.ResponseUri}
                        });
                }

            }
            catch (Exception ex)
            {
                AspectF.Define.Do<NCrawlerEntitiesDbServices>(e =>
                {
                    e.ExecuteStoreCommand("delete Crawlqueue where [key] ={0}", crawlHistory.Key);
                });
                log4net.Config.XmlConfigurator.Configure();
                log4net.ILog log = log4net.LogManager.GetLogger("logger-name");
                log.Error(ex);
            }
        }

        #endregion

        #region Class Methods

        private static bool IsHtmlContent(string contentType)
        {
            return contentType.StartsWith("text/html", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }

}