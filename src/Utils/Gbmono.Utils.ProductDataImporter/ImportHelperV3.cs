using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gbmono.Common;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using Gbmono.IO;
using NLog;

namespace Gbmono.Utils.ProductDataImporter
{
    public class ImportHelperV3
    {
        static int[] imageNameAllowLengh = new int[] { 10, 15 };

        static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static readonly RepositoryManager _repositoryManager = new RepositoryManager();

        private static List<string> allowLocalName = new List<string>() { "r", "t" };
        private static List<string> secondaryNameBlankList = new List<string>() { "-", "" };

        private static readonly string WorkingDirectory = ConfigurationSettings.AppSettings["sourceFilesFolder"];
        public static void Load(FileInfo file)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(file.FullName, false))
            {
                string version = string.Empty;
                WorkbookPart wbPart = document.WorkbookPart;
                List<Sheet> sheets = wbPart.Workbook.Descendants<Sheet>().ToList();
                var sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault(c => c.Name == ConfigurationSettings.AppSettings["sheetName"]);
                if (sheet == null)
                {
                    sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();
                }

                if (sheet == null)
                {
                    Logger.Log(LogLevel.Error, "Can not find sheet.");

                    // move the file into error folder
                    FileHelper.MoveFile(file.FullName, Path.Combine(WorkingDirectory + "\\error", file.Name));

                    return;
                }
                WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(sheet.Id);

                // import product data and return new product id
                var newProductId = Import(wbPart, wsPart, file);
                if (newProductId == null)
                {
                    // failed to import
                    // move file into error folder
                    FileHelper.MoveFile(file.FullName, Path.Combine(WorkingDirectory + "\\error", file.Name));
                    return;
                }

            };
        }

        static int? Import(WorkbookPart wbPart, WorksheetPart wsPart, FileInfo file)
        {
            var startIndex = 4;
            var index = startIndex;
            var successedCount = 0;
            while (true)
            {
                // 大分CD
                var categoryCodeLevel1 = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("B", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                if (categoryCodeLevel1 == "")
                {
                    break;
                }
                // 棚割CD
                var categoryCodeLevel2 = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("C", index)).RemoveEmptyOrWrapCharacters().ToDBC();

                // 中分CD
                var categoryCodeLevel3 = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("D", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                if (categoryCodeLevel3.Length > 2)
                {
                    // take the last 2 digits
                    categoryCodeLevel3 = categoryCodeLevel3.Substring(4, 2);
                }
                Logger.Log(LogLevel.Info, string.Format("Retreiving category by category code: {0}{1}{2}", categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3));

                //取引先CD
                var tradeCode = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("E", index)).RemoveEmptyOrWrapCharacters().ToDBC();

                //商品CD
                //var productCode = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("E", index)).RemoveEmptyOrWrapCharacters().ToDBC();

                //催事CD
                var promotionCode = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("F", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                if (promotionCode == "")
                {
                    promotionCode = null;
                }
                //クーポンCD
                var cuponCode = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("G", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                if (cuponCode == "")
                {
                    cuponCode = null;
                }
                //Topic CD
                var topicCode = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("H", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                if (topicCode == "")
                {
                    topicCode = null;
                }
                //中分類ランキングCD ?
                var nKnow = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("I", index)).RemoveEmptyOrWrapCharacters().ToDBC();

                //二维码
                var barCode = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("J", index)).RemoveEmptyOrWrapCharacters().ToDBC();


                //get category id
                var categoryId = GetCategoryId(categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3, barCode);
                if (categoryId == null)
                {
                    Logger.Log(LogLevel.Error, "Can not find matched category.");
                    return null;
                }

                //品牌 メーカー名
                var brandName = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("K", index)).RemoveEmptyOrWrapCharacters();
                var brandId = GetBrandId(brandName);

                //系列 ブランド名
                var brandCollectionName = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("L", index)).RemoveEmptyOrWrapCharacters();
                //var brandCollectionId = GetBrandId(brandName);

                //商品名1
                var primaryName = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("M", index)).RemoveEmptyOrWrapCharacters();
                //商品名2
                var secondaryName = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("N", index)).RemoveEmptyOrWrapCharacters();
                if (secondaryName == "")
                {
                    secondaryName = null;
                }
                //特征
                var flavor = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("O", index)).RemoveEmptyOrWrapCharacters();
                //容量
                var weight = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("P", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                //价格 希望小売価格
                var priceText = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("Q", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                // convert
                double price;
                if (!double.TryParse(Regex.Replace(priceText, @"[^0-9.]", string.Empty), out price))
                {
                    price = 0;
                }
                //产品特征
                var description = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("R", index)).Trim();
                //使用方法
                var instruction = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("S", index)).Trim();
                //追加
                var extraInformation = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("T", index)).Trim();


                // 激活日期 掲載開始日
                var activationDateText = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AN", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                DateTime activationDate = activationDateText.From1900();


                //下架时间  掲載終了日
                var expiryDateText = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AO", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                DateTime expiryDate = expiryDateText.From1900();

                //商品発売日 
                var 商品発売日 = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AP", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                DateTime 商品発売日Date = 商品発売日.From1900();

                //製造中止日
                var 製造中止日 = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AQ", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                DateTime 製造中止日Date = 製造中止日.From1900();

                //入数
                var amount = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AR", index)).RemoveEmptyOrWrapCharacters().ToDBC();

                //W
                var wData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AS", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                double wValue;
                Double.TryParse(wData, out wValue);
                //D
                var dData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AT", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                double dValue;
                Double.TryParse(dData, out dValue);
                //H
                var hData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AU", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                double hValue;
                Double.TryParse(hData, out hValue);
                //情報更新日
                var updateDataText = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("AW", index)).RemoveEmptyOrWrapCharacters().ToDBC();
                DateTime updateData = updateDataText.From1900();


                //春
                var springData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("CB", index)).Trim();
                bool? spring = GetSeason(springData);

                //夏
                var summerData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("CC", index)).Trim();
                bool? summer = GetSeason(summerData);

                //秋
                var autumnData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("CD", index)).Trim();
                bool? autumn = GetSeason(autumnData);

                //冬
                var winterData = GetCellPathValue(wbPart, wsPart, Util.IndexAppend("CE", index)).Trim();
                bool? winter = GetSeason(winterData);

                var newProduct = new Product
                {
                    //Todo
                    CategoryId = (int)categoryId,
                    BrandId = brandId,
                    BrandCollectionName = brandCollectionName,
                    PrimaryName = primaryName,
                    SecondaryName = secondaryName,
                    BarCode = barCode,
                    Flavor = flavor,
                    Weight = weight,
                    Width = wValue,
                    Depth = hValue,
                    Height = dValue,
                    //ProductCode = productCode,
                    PromotionCode = promotionCode,
                    CuponCode = cuponCode,
                    TopicCode = topicCode,
                    Description = description,
                    Instruction = instruction,
                    ExtraInformation = extraInformation,
                    // CountryId = 1, // take out contry id from product table
                    CreatedDate = DateTime.Now,
                    UpdatedDate = updateData,
                    ActivationDate = activationDate,
                    //ExpiryDate = expiryDate
                    Spring = spring,
                    Summer = summer,
                    Autumn = autumn,
                    Winter = winter,
                    Price = price,
                    IsPublished = true
                };
                index++;

                // check if any product with same barcode exists in db
                if (_repositoryManager.ProductRepository.Table.Any(m => m.BarCode == barCode))
                {
                    Logger.Log(LogLevel.Error, string.Format("Barcode: {0} already exists", barCode));
                    continue;
                }

                try
                {
                    _repositoryManager.ProductRepository.Create(newProduct);
                    _repositoryManager.ProductRepository.Save();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                    throw;
                }
              

                //BrandCollectionCheck
                BrandCollectionCheck(newProduct);

                successedCount++;
            }
            return successedCount;
        }


        private static string GetCellPathValue(WorkbookPart wbPart, WorksheetPart wsPart, string cellPath)
        {
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference.Value == cellPath).FirstOrDefault();

            string type = string.Empty;
            if (theCell != null)
            {
                return GetCellValue(wbPart, theCell);
            }

            return null;
        }

        static string GetCellValue(WorkbookPart wbPart, Cell theCell)
        {
            string value = theCell.InnerText;
            if (theCell.DataType != null)
            {
                switch (theCell.DataType.Value)
                {
                    case CellValues.SharedString:
                        var stringTable = wbPart.
                          GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        if (stringTable != null)
                        {
                            //var allNoAttributesValue = stringTable.SharedStringTable.
                            //    ElementAt(int.Parse(value)).Where(m => m.HasAttributes == false).Select(m => m.InnerText).Aggregate((m1, m2) => m1 + m2);
                            //value = allNoAttributesValue;

                            //var a = stringTable.SharedStringTable.
                            //        ElementAt(int.Parse(value));
                            //var node = stringTable.SharedStringTable.
                            //    ElementAt(int.Parse(value)).First();

                            //var nodeA = stringTable.SharedStringTable.
                            //    ElementAt(int.Parse(value));

                            var allAllLocalNameNode = stringTable.SharedStringTable.
                                ElementAt(int.Parse(value)).Where(m => allowLocalName.Contains(m.LocalName)).ToList();
                            if (allAllLocalNameNode.Any())
                            {
                                value= allAllLocalNameNode.Select(m => m.InnerText).Aggregate((m1, m2) => m1 + m2);
                            }
                            else
                            {
                                value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                            }
                         
                            //var a = System.Text.RegularExpressions.Regex.Replace(fieldFirstChild.OuterXml, "<[^>]*>", "");

                            //!!Old first field
                            //var fieldFirstChild = stringTable.SharedStringTable.
                            //    ElementAt(int.Parse(value)).FirstChild;
                            //if (fieldFirstChild != null)
                            //{
                            //    value = fieldFirstChild.InnerText;
                            //}
                            //else
                            //{
                            //    value = stringTable.SharedStringTable.
                            //      ElementAt(int.Parse(value)).InnerText;
                            //}
                        }
                        break;

                    case CellValues.Boolean:
                        switch (value)
                        {
                            case "0":
                                value = "FALSE";
                                break;
                            default:
                                value = "TRUE";
                                break;
                        }
                        break;
                    case CellValues.String:
                        if (theCell.CellValue != null)
                            value = theCell.CellValue.InnerText;
                        break;
                }
            }
            return value;
        }

        static void BrandCollectionCheck(Product product)
        {
            //if (!secondaryNameBlankList.Contains(product.SecondaryName))    
            //{
            var brandCollection =
                _repositoryManager.BrandCollectionRepository.Table.FirstOrDefault(
                    m => m.BrandId == product.BrandId && m.Name == product.BrandCollectionName);
            if (brandCollection == null)
            {
                brandCollection = new BrandCollection()
                {
                    BrandId = product.BrandId,
                    DisplayName = product.BrandCollectionName,
                    Name = product.BrandCollectionName
                };

                _repositoryManager.BrandCollectionRepository.Create(brandCollection);
                _repositoryManager.BrandCollectionRepository.Save();
            }


            product.BrandCollectionId = brandCollection.BrandCollectionId;
            _repositoryManager.ProductRepository.Save();

            //}
        }

        public static void ImportImage(string folderPath)
        {
            var imageFolder = new DirectoryInfo(folderPath);
            var images = imageFolder.GetFiles();

            if (images.Any())
            {
                foreach (var img in images)
                {
                    var imageName = img.Name;
                    var imageExtension = Path.GetExtension(imageName);

                    var imageNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName);

                    if (!imageNameAllowLengh.Contains(imageNameWithoutExtension.Length))
                    {
                        continue;
                    }

                    var barcode = imageNameWithoutExtension.Substring(0, imageNameWithoutExtension.Length - 2);
                    var product = _repositoryManager.ProductRepository.Table.FirstOrDefault(m => m.BarCode == barcode);
                    if (product != null)
                    {
                        var imageFileFolder = GetImageFolderByBarcode(barcode);
                        var imageCatePath = $@"{barcode}";

                        var productId = product.ProductId;
                        var imageIndex = imageNameWithoutExtension.Substring(imageNameWithoutExtension.Length-2, 2);

                        string filename = string.Format(@"{0}{1}", imageIndex, imageExtension);
                        string filePath = string.Format(@"{0}/{1}", imageFileFolder, filename);

                        var storeFileName = $@"{imageCatePath}/{filename}";
                        if (!_repositoryManager.ProductImageRepository.Table.Any(m => m.ProductId == product.ProductId && m.FileName == storeFileName))
                        {
                            FileStream fileStream = new FileStream(img.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                            // 读取文件的 byte[]   
                            byte[] bytes = new byte[fileStream.Length];
                            fileStream.Read(bytes, 0, bytes.Length);
                            fileStream.Close();
                            File.WriteAllBytes(filePath, bytes);
                            //Todo ProductImageTypeId is temp
                            var newProductImage = new ProductImage()
                            {
                                ProductId = productId,
                                //FileName = filePath,
                                FileName = storeFileName,
                                ProductImageTypeId = 1
                            };
                            _repositoryManager.ProductImageRepository.Create(newProductImage);
                            _repositoryManager.ProductImageRepository.Save();
                        }
                        else
                        {
                            Console.WriteLine("ProductId:" + productId + "'s Image:" + storeFileName + "Existed!");
                        }

                    }
                    else
                    {
                        //Todo error
                    }


                }
            }


            //var imageFileFolder = GetImageFolderByCategory(categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3);
            //var imageCatePath = $@"{categoryCodeLevel1}/{categoryCodeLevel2}/{categoryCodeLevel3}";
            //int imageIndex = 1;
            //var success = 0;
            //var fail = 0;
            //foreach (ImagePart i in wsPart.DrawingsPart.ImageParts)
            //{
            //    try
            //    {
            //        using (Stream stream = i.GetStream())
            //        {
            //            long length = stream.Length;
            //            byte[] byteStream = new byte[length];
            //            stream.Read(byteStream, 0, (int)length);

            //            var imageValidated = ImageHelper.ValidateImageQualityByPixel(stream);
            //            if (imageValidated)
            //            {

            //                var imageExtension = Path.GetExtension(i.Uri.ToString());
            //                string filename = string.Format(@"{0}_{1}{2}", productId, imageIndex, imageExtension);
            //                string filePath = string.Format(@"{0}/{1}", imageFileFolder, filename);

            //                File.WriteAllBytes(filePath, byteStream);
            //                //Todo ProductImageTypeId is temp
            //                var newProductImage = new ProductImage()
            //                {
            //                    ProductId = productId,
            //                    //FileName = filePath,
            //                    FileName = $@"{imageCatePath}/{filename}",
            //                    ProductImageTypeId = 1
            //                };
            //                _repositoryManager.ProductImageRepository.Create(newProductImage);
            //                _repositoryManager.ProductImageRepository.Save();
            //            }
            //            else
            //            {
            //                Console.WriteLine("Pass Image:productId:" + productId);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger.Log(LogLevel.Error, string.Format("Save Image Error:productId {0},ex:", productId));
            //        fail++;
            //    }
            //    imageIndex++;
            //}
        }

        private static string GetImageFolderByCategory(string categoryCodeLevel1, string categoryCodeLevel2,
          string categoryCodeLevel3)
        {
            var imageFileFolder = ConfigurationManager.AppSettings["imageFolder"];

            var finalPath = FileHelper.CreateDirectory(imageFileFolder, categoryCodeLevel1, categoryCodeLevel2,
                categoryCodeLevel3);

            return finalPath;
        }

        private static string GetImageFolderByBarcode(string barcode)
        {
            var imageFileFolder = ConfigurationManager.AppSettings["imageFolder"];

            var finalPath = FileHelper.CreateDirectoryProductImage(imageFileFolder, barcode);

            return finalPath;
        }


        static int GetBrandId(string brandName)
        {
            var brand = _repositoryManager.BrandRepository
                                          .Table
                                          .FirstOrDefault(m => m.Name == brandName);
            if (brand == null)
            {
                // create new brand
                brand = new Brand
                {
                    Name = brandName,
                    DisplayName = brandName,
                    Enabled = true
                };

                _repositoryManager.BrandRepository.Create(brand);
                _repositoryManager.BrandRepository.Save();
            }

            return brand.BrandId;
        }

        private static int? GetCategoryId(string topCateCode, string secondCateCode, string thirdCateCode, string barCode)
        {
            var category = _repositoryManager.CategoryRepository
                .Table
                .Include(m => m.ParentCategory.ParentCategory)
                .FirstOrDefault(m => m.CategoryCode == thirdCateCode &&
                                     m.ParentCategory.CategoryCode == secondCateCode &&
                                     m.ParentCategory.ParentCategory.CategoryCode == topCateCode);
            if (category == null)
            {
                Logger.Log(LogLevel.Error,
                    string.Format("barCode:{3} can not be match the category,Code l1:{0},l2:{1},l3:{2},ex:", topCateCode, secondCateCode, thirdCateCode, barCode));
                return null;
            }
            return category.CategoryId;
        }



        static int? GetCategoryId(string topCateCode, string secondCateCode, string thirdCateCode, string level1Name, string level2Name, string level3Name)
        {
            var category = _repositoryManager.CategoryRepository
                                             .Table
                                             .Include(m => m.ParentCategory.ParentCategory)
                                             .FirstOrDefault(m => m.CategoryCode == thirdCateCode &&
                                                                  m.ParentCategory.CategoryCode == secondCateCode &&
                                                                  m.ParentCategory.ParentCategory.CategoryCode == topCateCode);
            if (category == null)
            {
                try
                {
                    var categoryLevel1 =
                        _repositoryManager.CategoryRepository.Table.FirstOrDefault(
                            m => m.CategoryCode == topCateCode && m.ParentId == null);
                    if (categoryLevel1 == null)
                    {
                        categoryLevel1 = new Category() { CategoryCode = topCateCode, Name = level1Name, ParentId = null };
                        _repositoryManager.CategoryRepository.Create(categoryLevel1);
                        _repositoryManager.CategoryRepository.Save();
                    }

                    var categoryLevel2 =
                           _repositoryManager.CategoryRepository.Table.FirstOrDefault(
                               m => m.CategoryCode == secondCateCode && m.ParentId == categoryLevel1.CategoryId);
                    if (categoryLevel2 == null)
                    {
                        categoryLevel2 = new Category() { CategoryCode = secondCateCode, Name = level2Name, ParentId = categoryLevel1.CategoryId };
                        _repositoryManager.CategoryRepository.Create(categoryLevel2);
                        _repositoryManager.CategoryRepository.Save();
                    }


                    var categoryLevel3 = new Category() { CategoryCode = thirdCateCode, Name = level3Name, ParentId = categoryLevel2.CategoryId };
                    _repositoryManager.CategoryRepository.Create(categoryLevel3);
                    _repositoryManager.CategoryRepository.Save();

                    return categoryLevel3.CategoryId;
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.Error, string.Format("Create Category l1:{0},l2:{1},l3:{2},ex:", topCateCode, secondCateCode, thirdCateCode));
                    return null;
                }

            }

            return category.CategoryId;
        }
        private static bool? GetSeason(string data)
        {
            bool? result;
            switch (data)
            {
                case "1":
                    result = true;
                    break;
                case "0":
                    result = false;
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }



    }


}
