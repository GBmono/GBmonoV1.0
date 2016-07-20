using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.CrawlerDB;
using Gbmono.EF.DataContext;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
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
                var repo = new RepositoryManager();
                if (File.Exists(filePath))
                {
                    string title, corver, body, bodyHtml;


                    var info = new HtmlDocument();
                    info.LoadHtml(File.ReadAllText(filePath));
                    var fileId = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                    using (var db = new NCrawlerEntitiesDbServices())
                    {

                        var url = db.CrawlHistory.Single(m => m.Id == fileId).Key;
                        //if(repo.NArticleRepository.Table.Any(m=>m.SourceUrl==url))
                        //    return;

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

                        var bodyNode =
                            info.DocumentNode.SelectSingleNode(
                                "//article[@class='news-detail']//div[@class='body']//section//div[@class='inner']");
                        if (bodyNode != null)
                        {
                            body = bodyNode.InnerText;
                            bodyHtml = bodyNode.InnerHtml;
                        }
                        else
                        {
                            return;
                        }
                        var article = new NArticle()
                        {
                            Body = body,
                            BodyHtml = bodyHtml,
                            CorverUrl = corver,
                            CreateDate = DateTime.Now,
                            PublicshDate = DateTime.Now,
                            SourceUrl = url,
                            Title = title
                        };
                        //repo.NArticleRepository.Create(article);
                        //repo.NArticleRepository.Save();



                        var imagesNode = info.DocumentNode.SelectNodes("//article[@class='news-detail']//div[@class='body']//img");
                        if (imagesNode != null && imagesNode.Any())
                        {
                            foreach (var image in imagesNode)
                            {
                                var imageUrl = image.Attributes["src"].Value;

                                //repo.NArticleImageRepository.Create(new NArticleImage() { NArticleId = article.NArticleId, Url = imageUrl });
                            }
                            //repo.NArticleRepository.Save();
                        }

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
