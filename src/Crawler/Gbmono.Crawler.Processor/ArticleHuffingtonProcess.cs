using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Gbmono.CrawlerDB;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using HtmlAgilityPack;

namespace Gbmono.Crawler.Processor
{
    public class ArticleHuffingtonProcess
    {
        private static int groupId = -844136808;
        private static string htmlFilePath = @"C:\GbmonoCrawlerHtml\" + groupId;
        private static string domain = "http://www.huffingtonpost.jp/";

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
                    string title, bodyHtml;


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
                                "//h1[@class='title']");
                        if (titleNode != null)
                        {
                            title = titleNode.First().InnerText;
                        }
                        else
                        {
                            return;
                        }



                        var bodyNode =
                            info.DocumentNode.SelectSingleNode(
                                "//article[@class='entry']");
                        if (bodyNode != null)
                        {
                            bodyHtml = bodyNode.InnerHtml;
                        }
                        else
                        {
                            return;
                        }
                        var article = new Article()
                        {
                            Body = bodyHtml,
                            Title = title,
                            ArticleTypeId = 4,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = "Admin",
                            CreatedBy = "Admin",
                            ModifiedDate = DateTime.Now,
                            IsPublished = true
                        };
                        repo.ArticleRepository.Create(article);
                        repo.ArticleRepository.Save();

                        var articleFolder = "article";
                        var articleFolderS = articleFolder + "/" + article.ArticleId;
                        var articleFolderST = articleFolderS + "/thumbnails";
                        Directory.CreateDirectory(articleFolderST);

                        var imagesNode = info.DocumentNode.SelectNodes("//article[@class='entry']//img[contains(@src,'.jpg')]");
                        if (imagesNode != null && imagesNode.Any())
                        {
                            foreach (var image in imagesNode)
                            {
                                try
                                {


                                    var imageUrl = image.Attributes["src"].Value;
                                    var imageName = Guid.NewGuid();

                                    if (imageUrl != null)
                                    {
                                        var storePath = article.ArticleId + "/" + imageName + Path.GetExtension(imageUrl);
                                        var storeTPath = article.ArticleId + "/thumbnails/" + imageName + Path.GetExtension(imageUrl);

                                        var c = new WebClient();
                                        c.DownloadFile(imageUrl, articleFolder + "/" + storePath);
                                        c.DownloadFile(imageUrl, articleFolder + "/" + storeTPath);
                                        repo.ArticleImageRepository.Create(new ArticleImage()
                                        {
                                            ArticleId = article.ArticleId,
                                            Url = storePath,
                                            ThumbnailUrl = storeTPath,
                                            IsCoverImage = true
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                            repo.ArticleImageRepository.Save();
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

