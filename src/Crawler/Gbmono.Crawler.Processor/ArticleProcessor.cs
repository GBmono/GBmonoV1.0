using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.CrawlerDB;
using HtmlAgilityPack;

namespace Gbmono.Crawler.Processor
{
    public class ArticleProcessor
    {
        private static int groupId = -612019812;
        private static string htmlFilePath = @"C:\GbmonoCrawlerHtml\" + groupId;
        private static string domain = "http://www.houyhnhnm.jp/";

        public void Process()
        {
            try
            {
                //Process Data
                if (!Directory.Exists(htmlFilePath))
                {
                    Console.WriteLine("dir Wrong");
                    return;
                }
                var subHtmlFolders = Directory.GetDirectories(htmlFilePath);

                if (!subHtmlFolders.Any())
                {
                    Console.WriteLine("No Files");
                    return;
                }


                var allHtml = subHtmlFolders.SelectMany(m => Directory.GetFiles(m));
                Console.WriteLine("allHtml:" + allHtml.Count());


                Parallel.ForEach(allHtml, new ParallelOptions { MaxDegreeOfParallelism = 1 }, (html) =>
                {
                    ProcessSingleFile(html);
                });

            }
            catch (Exception ex)
            {

            }
        }

        private void ProcessSingleFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string title,corver;



                    var info = new HtmlDocument();
                    info.LoadHtml(File.ReadAllText(filePath));
                    var fileId = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                    using (var db = new NCrawlerEntitiesDbServices())
                    {

                        var url = db.CrawlHistory.Single(m => m.Id == fileId).Key;
                        if (db.ProductInfoes.Any(m => m.Url == url))
                        {
                            Console.WriteLine("Duplicate Url:" + url);
                            return;
                        }
                        //Todo add article
                        //var product = AddArticle(url);

                        var titleNode =
                            info.DocumentNode.SelectNodes(
                                "//head//title");
                        if (titleNode != null)
                        {
                            title = titleNode.First().InnerText;
                        }
                        else
                        {
                            return;
                        }

                        var imageNode =
                            info.DocumentNode.SelectSingleNode(
                                "//meta[@property='og:image']");
                        if (imageNode != null)
                        {
                            corver = imageNode.Attributes["content"].Value;
                        }
                        else
                        {
                            return;
                        }



                        db.SaveChanges();
                        Console.Write("+");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("-");
                Console.WriteLine("Single product Error:" + ex);
            }
        }



    }
}
