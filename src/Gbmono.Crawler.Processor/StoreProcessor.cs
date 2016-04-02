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
    public class StoreProcessor
    {
        private static int groupId = 1439170004;
        //private static int groupId = 301315303;
        private static string htmlFilePath = @"C:\GbmonoCrawlerHtml\" + groupId;
        private static string domain = "http://www.e-map.ne.jp/p/matukiyo/";
        private static List<string> keywordList = new List<string>() { "店铺名", "地址", "电话", "营业时间", "休息日", "设施服务", "商品类型", "结算方式", "都道府県", "市","lat","long" };
        private static List<KeywordType> keywordTypeList = new List<KeywordType>();

        private static List<string> add12 = new List<string>() { "都", "道", "府", "県", "市","区","郡" };

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
                    string name, address, tel, openTime;
                    List<string> closeday = new List<string>();
                


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

                        var nameNode = info.DocumentNode.SelectSingleNode("//table//tr//th[contains(text(),'店舗名')]/following-sibling::td//p");
                        if (nameNode != null)
                        {
                            name = nameNode.InnerText;

                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "店铺名").KeywordTypeId,
                                Value = name
                            });
                        }
                        else
                        {
                            return;
                        }

                        var addressNode = info.DocumentNode.SelectSingleNode("//table//tr//th[contains(text(),'住所')]/following-sibling::td//p");
                        if (addressNode != null)
                        {
                            address = addressNode.InnerText;

                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "地址").KeywordTypeId,
                                Value = address
                            });
                        }
                        else
                        {
                            return;
                        }
                        var address12Splic = address.Split(add12.ToArray(),StringSplitOptions.RemoveEmptyEntries).ToArray();
                        var add1 = address12Splic[0];
                        var add2 = address12Splic[1];
                        db.ProductKeywords.AddObject(new ProductKeyword()
                        {
                            ProductId = product.ProductInfoId,
                            KeywordTypeId = keywordTypeList.Single(m => m.Name == "都道府県").KeywordTypeId,
                            Value = add1
                        });
                        db.ProductKeywords.AddObject(new ProductKeyword()
                        {
                            ProductId = product.ProductInfoId,
                            KeywordTypeId = keywordTypeList.Single(m => m.Name == "市").KeywordTypeId,
                            Value = add2
                        });

                        var locationNode = info.DocumentNode.SelectSingleNode("//body").Attributes["onload"].Value.Split(';')[0].Replace("ZdcEmapInit","").Replace("'","").Replace("(","").Replace(")","").Split(',');
                        db.ProductKeywords.AddObject(new ProductKeyword()
                        {
                            ProductId = product.ProductInfoId,
                            KeywordTypeId = keywordTypeList.Single(m => m.Name == "lat").KeywordTypeId,
                            Value = locationNode[0]
                        });
                        db.ProductKeywords.AddObject(new ProductKeyword()
                        {
                            ProductId = product.ProductInfoId,
                            KeywordTypeId = keywordTypeList.Single(m => m.Name == "long").KeywordTypeId,
                            Value = locationNode[1]
                        });


                        var telNode = info.DocumentNode.SelectSingleNode("//table//tr//th[contains(text(),'電話番号')]/following-sibling::td//p");
                        if (telNode != null)
                        {
                            tel = telNode.InnerText;

                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "电话").KeywordTypeId,
                                Value = tel
                            });
                        }
                        else
                        {
                            return;
                        }
                        var openTimeNode = info.DocumentNode.SelectSingleNode("//table//tr//th[contains(text(),'営業時間')]/following-sibling::td//p[@class='opentime']");
                        if (openTimeNode != null)
                        {
                            openTime = openTimeNode.InnerText;

                            db.ProductKeywords.AddObject(new ProductKeyword()
                            {
                                ProductId = product.ProductInfoId,
                                KeywordTypeId = keywordTypeList.Single(m => m.Name == "营业时间").KeywordTypeId,
                                Value = openTime
                            });
                        }
                        else
                        {
                            return;
                        }
                        var closeDayNode = info.DocumentNode.SelectSingleNode("//table//tr//th[contains(text(),'定休日')]/following-sibling::td");
                        if (closeDayNode != null)
                        {
                            closeday = closeDayNode.InnerHtml.Split(new string[] { "・" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            if (closeday.Any())
                            {
                                foreach (var i in closeday)
                                {
                                    db.ProductKeywords.AddObject(new ProductKeyword()
                                    {
                                        ProductId = product.ProductInfoId,
                                        KeywordTypeId = keywordTypeList.Single(m => m.Name == "休息日").KeywordTypeId,
                                        Value = i.StripHtml().Trim()
                                    });
                                }
                            }
                        }
                        var serviceNode = info.DocumentNode.SelectNodes("//table//tr//th[contains(text(),'施設・サービス')]/following-sibling::td//p");
                        if (serviceNode != null)
                        {
                            foreach (var i in serviceNode)
                            {
                                db.ProductKeywords.AddObject(new ProductKeyword()
                                {
                                    ProductId = product.ProductInfoId,
                                    KeywordTypeId = keywordTypeList.Single(m => m.Name == "设施服务").KeywordTypeId,
                                    Value = i.InnerHtml.StripHtml().Trim()
                                });
                            }
                        }
                        var productNode = info.DocumentNode.SelectNodes("//table//tr//th[contains(text(),'取扱商品')]/following-sibling::td//p");
                        if (productNode != null)
                        {
                            foreach (var i in productNode)
                            {
                                db.ProductKeywords.AddObject(new ProductKeyword()
                                {
                                    ProductId = product.ProductInfoId,
                                    KeywordTypeId = keywordTypeList.Single(m => m.Name == "商品类型").KeywordTypeId,
                                    Value = i.InnerHtml.StripHtml().Trim()
                                });
                            }
                        }

                        var payNode = info.DocumentNode.SelectNodes("//table//tr//th[contains(text(),'決済方法')]/following-sibling::td//p");
                        if (payNode != null)
                        {
                            foreach (var i in payNode)
                            {
                                db.ProductKeywords.AddObject(new ProductKeyword()
                                {
                                    ProductId = product.ProductInfoId,
                                    KeywordTypeId = keywordTypeList.Single(m => m.Name == "结算方式").KeywordTypeId,
                                    Value = i.InnerHtml.StripHtml().Trim()
                                });
                            }
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


    public class StoreMapping
    {




    }


}
