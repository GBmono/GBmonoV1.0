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
    public class ImportHelperV2
    {
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static readonly RepositoryManager _repositoryManager = new RepositoryManager();

        private static List<string> secondaryNameBlankList = new List<string>() { "-", "" };

        private static readonly string WorkingDirectory = ConfigurationSettings.AppSettings["sourceFilesFolder"];
        public static void Load(FileInfo file)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(file.FullName, false))
            {
                string version = string.Empty;
                WorkbookPart wbPart = document.WorkbookPart;
                List<Sheet> sheets = wbPart.Workbook.Descendants<Sheet>().ToList();
                var sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault(c => c.Name == ConfigurationManager.AppSettings["sheetName"]);
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
            // load brand name from cell
            var brandName = GetCellPathValue(wbPart, wsPart, "I6").RemoveEmptyOrWrapCharacters();

            if (string.IsNullOrEmpty(brandName))
            {
                Logger.Log(LogLevel.Error, "Can not find brand name or invalid name");

                return null;
            }

            Logger.Log(LogLevel.Info, "Retreiving brand by brand name: " + brandName);

            // get the brand id
            var brandId = GetBrandId(brandName);

            // 大分类CD
            var categoryCodeLevel1 = GetCellPathValue(wbPart, wsPart, "P9").RemoveEmptyOrWrapCharacters().ToDBC();
            var categoryCodeLevel1Name = GetCellPathValue(wbPart, wsPart, "T9").RemoveEmptyOrWrapCharacters().ToDBC();
            // 陈列标准CD
            var categoryCodeLevel2 = GetCellPathValue(wbPart, wsPart, "Z9").RemoveEmptyOrWrapCharacters().ToDBC();
            var categoryCodeLevel2Name = GetCellPathValue(wbPart, wsPart, "AD9").RemoveEmptyOrWrapCharacters().ToDBC();
            // 中品类CD
            var categoryCodeLevel3 = GetCellPathValue(wbPart, wsPart, "AJ9").RemoveEmptyOrWrapCharacters().ToDBC();
            var categoryCodeLevel3Name = GetCellPathValue(wbPart, wsPart, "AN9").RemoveEmptyOrWrapCharacters().ToDBC();
            if (categoryCodeLevel3.Length > 2)
            {
                // take the last 2 digits
                categoryCodeLevel3 = categoryCodeLevel3.Substring(4, 2);
            }

            Logger.Log(LogLevel.Info, string.Format("Retreiving category by category code: {0}{1}{2}", categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3));

            // get category id
            var categoryId = GetCategoryId(categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3, categoryCodeLevel1Name, categoryCodeLevel2Name, categoryCodeLevel3Name);
            if (categoryId == null)
            {
                Logger.Log(LogLevel.Error, "Can not find matched category.");

                return null;
            }

            //价格
            var priceText = GetCellPathValue(wbPart, wsPart, "AP6").RemoveEmptyOrWrapCharacters().ToDBC();
            // convert
            double price;
            if (!double.TryParse(Regex.Replace(priceText, @"[^0-9.]", string.Empty), out price))
            {
                price = 0;
            }


            // 激活日期
            var activationDateText = GetCellPathValue(wbPart, wsPart, "H44").RemoveEmptyOrWrapCharacters().ToDBC();
            DateTime activationDate = activationDateText.From1900();


            //下架时间
            var expiryDateText = GetCellPathValue(wbPart, wsPart, "H45").RemoveEmptyOrWrapCharacters().ToDBC();
            DateTime expiryDate = expiryDateText.From1900();

            //商品名1
            var primaryName = GetCellPathValue(wbPart, wsPart, "N6").RemoveEmptyOrWrapCharacters();
            //商品名2
            var secondaryName = GetCellPathValue(wbPart, wsPart, "X6").RemoveEmptyOrWrapCharacters();
            //二维码
            var barCode = GetCellPathValue(wbPart, wsPart, "B6").RemoveEmptyOrWrapCharacters().ToDBC();
            //特征
            var flavor = GetCellPathValue(wbPart, wsPart, "AH6").RemoveEmptyOrWrapCharacters();
            //容量
            var weight = GetCellPathValue(wbPart, wsPart, "AL6").RemoveEmptyOrWrapCharacters().ToDBC();
            //商品CD
            var productCode = GetCellPathValue(wbPart, wsPart, "AJ11").RemoveEmptyOrWrapCharacters().ToDBC();
            //促销CD
            var promotionCode = GetCellPathValue(wbPart, wsPart, "P14").RemoveEmptyOrWrapCharacters().ToDBC();
            //优惠CD
            var cuponCode = GetCellPathValue(wbPart, wsPart, "T14").RemoveEmptyOrWrapCharacters().ToDBC();
            //Topic CD
            var topicCode = GetCellPathValue(wbPart, wsPart, "AE14").RemoveEmptyOrWrapCharacters().ToDBC();
            //产品特征
            var description = GetCellPathValue(wbPart, wsPart, "P17").Trim();
            //使用方法
            var instruction = GetCellPathValue(wbPart, wsPart, "B26").Trim();
            //追加
            var extraInformation = GetCellPathValue(wbPart, wsPart, "B40").Trim();

            //春
            var springData = GetCellPathValue(wbPart, wsPart, "V62").Trim();
            bool? spring = GetSeason(springData);

            //夏
            var summerData = GetCellPathValue(wbPart, wsPart, "W62").Trim();
            bool? summer = GetSeason(summerData);

            //秋
            var autumnData = GetCellPathValue(wbPart, wsPart, "X62").Trim();
            bool? autumn = GetSeason(autumnData);

            //冬
            var winterData = GetCellPathValue(wbPart, wsPart, "Y62").Trim();
            bool? winter = GetSeason(winterData);

            //W
            var wData = GetCellPathValue(wbPart, wsPart, "G58").RemoveEmptyOrWrapCharacters().ToDBC();
            double wValue;
            Double.TryParse(wData, out wValue);
            //H
            var hData = GetCellPathValue(wbPart, wsPart, "M58").RemoveEmptyOrWrapCharacters().ToDBC();
            double hValue;
            Double.TryParse(hData, out hValue);
            //D
            var dData = GetCellPathValue(wbPart, wsPart, "J58").RemoveEmptyOrWrapCharacters().ToDBC();
            double dValue;
            Double.TryParse(dData, out dValue);

            // new product instance
            var newProduct = new Product
            {
                CategoryId = categoryId.Value,
                BrandId = brandId,
                PrimaryName = primaryName,
                SecondaryName = secondaryName,
                BarCode = barCode,
                Flavor = flavor,
                Weight = weight,
                Width = wValue,
                Depth = hValue,
                Height = dValue,
                ProductCode = productCode,
                PromotionCode = promotionCode,
                CuponCode = cuponCode,
                TopicCode = topicCode,
                Description = description,
                Instruction = instruction,
                ExtraInformation = extraInformation,
                // CountryId = 1, // take out contry id from product table
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ActivationDate = activationDate,
                //ExpiryDate = expiryDate
                Spring = spring,
                Summer = summer,
                Autumn = autumn,
                Winter = winter,
                Price = price,
                IsPublished = true
            };

            // check if any product with same barcode exists in db
            if (_repositoryManager.ProductRepository.Table.Any(m => m.BarCode == barCode))
            {
                Logger.Log(LogLevel.Error, string.Format("Barcode: {0} already exists", barCode));
                return null;
            }

            _repositoryManager.ProductRepository.Create(newProduct);
            _repositoryManager.ProductRepository.Save();

            //BrandCollectionCheck
            BrandCollectionCheck(newProduct);


            //ImportImage
            ImportImage(wsPart, newProduct.ProductId, categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3);


            return newProduct.ProductId;
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
                            var fieldFirstChild = stringTable.SharedStringTable.
                                ElementAt(int.Parse(value)).FirstChild;
                            if (fieldFirstChild != null)
                            {
                                value = fieldFirstChild.InnerText;
                            }
                            else
                            {
                                value = stringTable.SharedStringTable.
                                  ElementAt(int.Parse(value)).InnerText;
                            }
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
                }
            }
            return value;
        }

        static void BrandCollectionCheck(Product product)
        {
            if (!secondaryNameBlankList.Contains(product.SecondaryName))
            {
                var brandCollection =
                    _repositoryManager.BrandCollectionRepository.Table.FirstOrDefault(
                        m => m.BrandId == product.BrandId && m.Name == product.PrimaryName);
                if (brandCollection == null)
                {
                    brandCollection = new BrandCollection()
                    {
                        BrandId = product.BrandId,
                        DisplayName = product.PrimaryName,
                        Name = product.PrimaryName
                    };

                    _repositoryManager.BrandCollectionRepository.Create(brandCollection);
                    _repositoryManager.BrandCollectionRepository.Save();
                }


                var productInstance = _repositoryManager.ProductRepository.Get(product.ProductId);
                productInstance.BrandCollectionId = brandCollection.BrandCollectionId;
                _repositoryManager.ProductRepository.Save();

            }
        }

        static void ImportImage(WorksheetPart wsPart, int productId, string categoryCodeLevel1, string categoryCodeLevel2, string categoryCodeLevel3)
        {
            var imageFileFolder = GetImageFolderByCategory(categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3);
            var imageCatePath = $@"{categoryCodeLevel1}/{categoryCodeLevel2}/{categoryCodeLevel3}";
            int imageIndex = 1;
            var success = 0;
            var fail = 0;
            foreach (ImagePart i in wsPart.DrawingsPart.ImageParts)
            {
                try
                {
                    using (Stream stream = i.GetStream())
                    {
                        long length = stream.Length;
                        byte[] byteStream = new byte[length];
                        stream.Read(byteStream, 0, (int)length);

                        var imageValidated = ImageHelper.ValidateImageQualityByPixel(stream);
                        if (imageValidated)
                        {

                            var imageExtension = Path.GetExtension(i.Uri.ToString());
                            string filename = string.Format(@"{0}_{1}{2}", productId, imageIndex, imageExtension);
                            string filePath = string.Format(@"{0}/{1}", imageFileFolder, filename);

                            File.WriteAllBytes(filePath, byteStream);
                            //Todo ProductImageTypeId is temp
                            var newProductImage = new ProductImage()
                            {
                                ProductId = productId,
                                //FileName = filePath,
                                FileName = $@"{imageCatePath}/{filename}",
                                ProductImageTypeId = 1
                            };
                            _repositoryManager.ProductImageRepository.Create(newProductImage);
                            _repositoryManager.ProductImageRepository.Save();
                        }
                        else
                        {
                            Console.WriteLine("Pass Image:productId:" + productId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.Error, string.Format("Save Image Error:productId {0},ex:", productId));
                    fail++;
                }
                imageIndex++;
            }
        }

        private static string GetImageFolderByCategory(string categoryCodeLevel1, string categoryCodeLevel2,
          string categoryCodeLevel3)
        {
            var imageFileFolder = ConfigurationManager.AppSettings["imageFolder"];

            var finalPath = FileHelper.CreateDirectory(imageFileFolder, categoryCodeLevel1, categoryCodeLevel2,
                categoryCodeLevel3);

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

    //Will move to unify util project
    public static class Util
    {
        public static DateTime From1900(this string days)
        {
            try
            {
                return new DateTime(1900, 1, 1).AddDays(double.Parse(days) - 2);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }


    }


}
