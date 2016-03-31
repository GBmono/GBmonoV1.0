using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gbmono.CrawlerDB;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using HtmlAgilityPack;

namespace Gbmono.Crawler.Processor
{
    public class Processor
    {
        private static int groupId = 1613303615;
        private static string htmlFilePath = @"C:\GbmonoCrawlerHtml\" + groupId;
        private static string domain = "http://item.rakuten.co.jp/sundrug/";
        private static List<string> keywordList = new List<string>() { "条形码", "効能効果", "用法用量", "商品区分", "剂形", "添加剂", "成分分量", "生产销售公司" };
        private static List<KeywordType> keywordTypeList = new List<KeywordType>();

        public void Process()
        {
            keywordTypeList = InitKeyword();
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

                var website = AddWebSite(groupId, domain);


                var allHtml = subHtmlFolders.SelectMany(m => Directory.GetFiles(m));
                Console.WriteLine("allHtml:" + allHtml.Count());


                Parallel.ForEach(allHtml, new ParallelOptions { MaxDegreeOfParallelism = 1 }, (html) =>
                  {
                      ProcessSingleFile(html, website.WebsiteId);
                  });

            }
            catch (Exception ex)
            {

            }
        }

        private void ProcessSingleFile(string filePath, int webSiteId)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string barcode, usage, distinguish, shape, saleCompany;
                    List<string> function = new List<string>();
                    List<string> additive = new List<string>();
                    List<string> component = new List<string>();


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
                        var product = AddProduct(url, webSiteId);

                        var barcodeNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'JAN')]/following-sibling::td");
                        if (barcodeNode != null)
                        {
                            barcode = barcodeNode.InnerText;

                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "条形码").KeywordTypeId,
                                Value = barcode
                            });
                        }
                        else
                        {
                            return;
                        }

                        var functionNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'効能・効果')]/following-sibling::td");
                        if (functionNode != null)
                        {
                            function = functionNode.InnerHtml.Split(new string[] { "<br>", "、", "・", "●", ",", "。",}, StringSplitOptions.RemoveEmptyEntries).ToList();

                            if (function.Any())
                            {
                                foreach (var i in function)
                                {
                                    db.ProductKeywords.AddObject(new ProductKeyword()
                                    {
                                        ProductId = product.ProductInfoId,
                                        KeywordTypeId = keywordTypeList.Single(m => m.Name == "効能効果").KeywordTypeId,
                                        Value = i.StripHtml().Trim()
                                    });
                                }
                            }
                        }

                        var usageNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'用法・用量')]/following-sibling::td");
                        if (usageNode != null)
                        {
                            usage = usageNode.InnerText;
                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "用法用量").KeywordTypeId,
                                Value = usage
                            });
                        }

                        var distinguishNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'商品区分')]/following-sibling::td");
                        if (distinguishNode != null)
                        {
                            distinguish = distinguishNode.InnerText;
                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "商品区分").KeywordTypeId,
                                Value = distinguish
                            });
                        }

                        var shapeNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'剤形')]/following-sibling::td");
                        if (shapeNode != null)
                        {
                            shape = shapeNode.InnerText;
                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "剂形").KeywordTypeId,
                                Value = shape
                            });
                        }

                        var additiveNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'添加物')]/following-sibling::td");
                        if (additiveNode != null)
                        {
                            additive = additiveNode.InnerHtml.Split(new string[2] { "<br>", "、" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (additive.Any())
                            {
                                foreach (var i in additive)
                                {
                                    db.ProductKeywords.AddObject(new ProductKeyword()
                                    {
                                        ProductId = product.ProductInfoId,
                                        KeywordTypeId = keywordTypeList.Single(m => m.Name == "添加剂").KeywordTypeId,
                                        Value = i
                                    });
                                }
                            }
                        }

                        var componenteNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'成分・分量')]/following-sibling::td");
                        if (componenteNode != null)
                        {
                            component = componenteNode.InnerHtml.Split(new string[2] { "<br>", "、" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (component.Any())
                            {
                                foreach (var i in component)
                                {
                                    db.ProductKeywords.AddObject(new ProductKeyword()
                                    {
                                        ProductId = product.ProductInfoId,
                                        KeywordTypeId = keywordTypeList.Single(m => m.Name == "成分分量").KeywordTypeId,
                                        Value = i
                                    });
                                }
                            }
                        }


                        var saleCompanyNode = info.DocumentNode.SelectSingleNode("//table//tr//td[contains(text(),'製造販売会社')]/following-sibling::td");
                        if (saleCompanyNode != null)
                        {
                            saleCompany = saleCompanyNode.InnerText;
                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "生产销售公司").KeywordTypeId,
                                Value = saleCompany
                            });
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


        private List<KeywordType> InitKeyword()
        {
            using (var db = new NCrawlerEntitiesDbServices())
            {
                foreach (var keyword in keywordList)
                {
                    if (!db.KeywordTypes.Any(m => m.Name == keyword))
                    {
                        db.KeywordTypes.AddObject(new KeywordType() { Name = keyword });
                    }
                }
                db.SaveChanges();
                return db.KeywordTypes.ToList();
            }
        }

        private Website AddWebSite(int groupId, string domain)
        {
            Website result;
            using (var db = new NCrawlerEntitiesDbServices())
            {
                result = db.Websites.SingleOrDefault(m => m.GroupId == groupId);
                if (result == null)
                {
                    result = new Website() { GroupId = groupId, Url = domain };
                    db.Websites.AddObject(result);
                    db.SaveChanges();
                }
                return result;
            }
        }

        private ProductInfo AddProduct(string url, int siteId)
        {
            ProductInfo result;
            using (var db = new NCrawlerEntitiesDbServices())
            {
                result = db.ProductInfoes.SingleOrDefault(m => m.Url == url);
                if (result == null)
                {
                    result = new ProductInfo() { WebsiteId = siteId, Url = url };
                    db.ProductInfoes.AddObject(result);
                    db.SaveChanges();
                }
                return result;
            }
        }

    }


    public class ProductMap
    {

        public void Mapping()
        {
            var _repoManager = new RepositoryManager();
            var punchedBarcode = new List<string>();
            using (var db = new NCrawlerEntitiesDbServices())
            {
                var productBarcode = _repoManager.ProductRepository.Table.Select(m => m.BarCode.ToString()).ToList();

                var crawlBarcode = db.ProductKeywords.Where(m => m.KeywordTypeId == 1).Select(m => m.Value.Substring(0, 13)).ToList();

                punchedBarcode = crawlBarcode.Intersect(productBarcode).ToList();

                Console.WriteLine(punchedBarcode.Count);
            }

            foreach (var barcode in punchedBarcode)
            {
                using (var db = new NCrawlerEntitiesDbServices())
                {
                    var productId =
                        db.ProductKeywords.First(m => m.KeywordTypeId == 1 && m.Value.Substring(0, 13) == barcode)
                            .ProductId;
                    var functions = db.ProductKeywords.Where(m => m.KeywordTypeId == 2 && m.ProductId == productId).ToList();

                    var pId = _repoManager.ProductRepository.Table.First(m => m.BarCode == barcode).ProductId;
                    if (functions.Any())
                    {
                        foreach (var f in functions)
                        {
                            if (f.Value.Length<20)
                            {
                                var tag = _repoManager.TagRepository.Table.FirstOrDefault(m => m.Name == f.Value);
                                if (tag == null)
                                {
                                    tag = new Tag() { Name = f.Value };
                                    _repoManager.TagRepository.Create(tag);
                                    _repoManager.TagRepository.Save();
                                    Console.Write("+");
                                }
                                else
                                {
                                    Console.Write("-");
                                }
                                if (!_repoManager.ProductTagRepository.Table.Any(m=>m.ProductId==pId&&m.TagId==tag.TagId))
                                {
                                    _repoManager.ProductTagRepository.Create(new ProductTag() { ProductId = pId, TagId = tag.TagId });
                                    _repoManager.ProductRepository.Save();
                                }
                              
                            }
                        }
                    }
                    Console.WriteLine("!");
                }
            }
        }
    }


    public static class Commom
    {
        public static string StripHtml(this string originalHtml)
        {
            return Regex.Replace(originalHtml, @"<(.|\n)*?>",
                string.Empty).Replace("\n", string.Empty);
        }


    }
}
