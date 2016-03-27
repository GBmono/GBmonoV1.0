using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using Gbmono.CrawlerDB.DbServices;
using NCrawler;
using NCrawler.Extensions;
using NCrawler.Interfaces;
using GbmonoCrawlerDB.Extensions;
using NCrawler.Utils;


namespace Gbmono.CrawlerDB
{
    public class CrawlUsingDbStorage
    {
        #region Class Methods

        public static void Run()
        {
            Console.Out.WriteLine("Simple crawl demo using local database a storage");

            var targetToCrawl = ConfigurationManager.AppSettings["CrawlTargetUrl"];
            var maximumThreadCount = int.Parse(ConfigurationManager.AppSettings["MaximumThreadCount"]);
            var maximumCrawlDepth = int.Parse(ConfigurationManager.AppSettings["MaximumCrawlDepth"]);

            // Setup crawler to crawl http://ncrawler.codeplex.com
            // with 1 thread adhering to robot rules, and maximum depth
            // of 2 with 4 pipeline steps:
            //	* Step 1 - The Html Processor, parses and extracts links, text and more from html
            //  * Step 2 - Processes PDF files, extracting text
            //  * Step 3 - Try to determine language based on page, based on text extraction, using google language detection
            //  * Step 4 - Dump the information to the console, this is a custom step, see the DumperStep class
            DbServicesModule.Setup(true);
            using (Crawler c = new Crawler(new Uri(targetToCrawl),
                new WholeHtmlProcessor(), // Process html
                new DumperStep())
            {
                // Custom step to visualize crawl
                MaximumThreadCount = maximumThreadCount,
                MaximumCrawlDepth = maximumCrawlDepth,
                ExcludeFilter = Program.ExtensionsToSkip,
            })
            {
                AspectF.Define.Do<NCrawlerEntitiesDbServices>(e =>
                {
                    if (e.CrawlQueue.Any())
                    {
                        var uri = new Uri(targetToCrawl);
                        
                        var groupId = uri.GetHashCode();
                        Console.Out.WriteLine("GroupId=" + groupId);
                        e.ExecuteStoreCommand("Update CrawlQueue set Exclusion='false' where GroupId={0} and Exclusion='true'", groupId);
                        //var exclusion = e.CrawlQueue.Where(m => m.Exclusion && m.GroupId == groupId).ToList();
                        //if (exclusion.Any())
                        //{
                        //    Console.Out.WriteLine("Count with Exclusion=" + exclusion.Count);
                        //    exclusion.ForEach(m => m.Exclusion = false);
                        //}
                        ////foreach (var crawlQueue in e.CrawlQueue)
                        ////{
                        ////    crawlQueue.Exclusion = false;
                        ////}
                        //e.SaveChanges();
                    }
                });
                // Begin crawl
                Console.Out.WriteLine(" Begin crawl");
                c.Crawl();
            }
        }

        #endregion
    }

    #region Nested type: DumperStep

    /// <summary>
    /// Custom pipeline step, to dump url to console
    /// </summary>
    internal class DumperStep : IPipelineStep
    {
        #region IPipelineStep Members

        /// <summary>
        /// </summary>
        /// <param name="crawler">
        /// The crawler.
        /// </param>
        /// <param name="propertyBag">
        /// The property bag.
        /// </param>
        public void Process(Crawler crawler, PropertyBag propertyBag)
        {
            CultureInfo contentCulture = (CultureInfo)propertyBag["LanguageCulture"].Value;
            string cultureDisplayValue = "N/A";
            if (!contentCulture.IsNull())
            {
                cultureDisplayValue = contentCulture.DisplayName;
            }

            lock (this)
            {
                Console.Out.WriteLine(ConsoleColor.Gray, "Url: {0}", propertyBag.Step.Uri);
                Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tContent type: {0}", propertyBag.ContentType);
                Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tContent length: {0}", propertyBag.Text.IsNull() ? 0 : propertyBag.Text.Length);
                Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tDepth: {0}", propertyBag.Step.Depth);
                Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tCulture: {0}", cultureDisplayValue);
                Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tThreadId: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
                Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tThread Count: {0}", crawler.ThreadsInUse);
                Console.Out.WriteLine();
            }
        }

        #endregion
    }

    #endregion
}