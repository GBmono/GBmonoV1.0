using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using NLog;
using Gbmono.IO;
using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Common;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Gbmono.Utils.ProductDataImporter
{
    class Program
    {
        // NLog instance
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // repo manager
        static readonly RepositoryManager _repositoryManager = new RepositoryManager();
        
        // working directory
        static readonly string WorkingDirectory = Path.GetFullPath(@"..\..") + "\\files\\";

        static void Main(string[] args)
        {
            // load all excel files from the folder
            var dataFiles = FileHelper.GetFiles(WorkingDirectory, new string[] { "xlsx" });

            // load each file
            foreach(var file in dataFiles)
            {
                Logger.Log(LogLevel.Info, "Importing data from : " + file.FullName);

                try
                {
                    // load
                    Load(file);

                    // move file into success folder when it finishes
                    FileHelper.MoveFile(file.FullName, Path.Combine(WorkingDirectory + "\\success", file.Name));
                }
                catch(Exception exp)
                {
                    var baseExp = exp.GetBaseException();

                    Logger.Log(LogLevel.Error, baseExp.Message);
                    Logger.Log(LogLevel.Error, baseExp.StackTrace);

                    // move file into error folder
                    FileHelper.MoveFile(file.FullName, Path.Combine(WorkingDirectory + "\\error", file.Name));
                }

            }

        }

        static void Load(FileInfo file)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(file.FullName, false))
            {
                string version = string.Empty;
                WorkbookPart wbPart = document.WorkbookPart;
                List<Sheet> sheets = wbPart.Workbook.Descendants<Sheet>().ToList();
                var sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault(c => c.Name == ConfigurationManager.AppSettings["sheetName"]);
                WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(sheet.Id);

                if (sheet == null)
                {
                    Logger.Log(LogLevel.Error, "Can not find sheet.");

                    // move the file into error folder
                    FileHelper.MoveFile(file.FullName, Path.Combine(WorkingDirectory + "\\error", file.Name));

                    return;
                }

                // import product data and return new product id
                var newProductId = Import(wbPart, wsPart, file);
                if (newProductId == null)
                {
                    // failed to import
                    // move file into error folder
                    FileHelper.MoveFile(file.FullName, Path.Combine(WorkingDirectory + "\\error", file.Name));
                    return;
                }

                // todo: import product images 
                // save image and insert into product image table
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
            // 陈列标准CD
            var categoryCodeLevel2 = GetCellPathValue(wbPart, wsPart, "Z9").RemoveEmptyOrWrapCharacters().ToDBC();
            // 中品类CD
            var categoryCodeLevel3 = GetCellPathValue(wbPart, wsPart, "AJ9").RemoveEmptyOrWrapCharacters().ToDBC();
            if(categoryCodeLevel3.Length > 2)
            {
                // take the last 2 digits
                categoryCodeLevel3 = categoryCodeLevel3.Substring(4, 2);
            }

            Logger.Log(LogLevel.Info, string.Format("Retreiving category by category code: {0}{1}{2}", categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3));

            // get category id
            var categoryId = GetCategoryId(categoryCodeLevel1, categoryCodeLevel2, categoryCodeLevel3);
            if(categoryId == null)
            {
                Logger.Log(LogLevel.Error, "Can not find matched category.");

                return null;
            }

            //价格
            var priceText = GetCellPathValue(wbPart, wsPart, "AP6").RemoveEmptyOrWrapCharacters().ToDBC();
            // convert
            double price;
            if(!double.TryParse(Regex.Replace(priceText, @"[^0-9.]", string.Empty), out price))
            {
                price = 0;
            }

            // 上架日期
            var activationDateText = GetCellPathValue(wbPart, wsPart, "T62").RemoveEmptyOrWrapCharacters().ToDBC();
            // convert
            DateTime activationDate;
            if(!DateTime.TryParse(activationDateText, out activationDate))
            {
                activationDate = DateTime.Today;
            }

            //下架时间
            var expiryDateText = GetCellPathValue(wbPart, wsPart, "AL62").RemoveEmptyOrWrapCharacters().ToDBC();
            // convert
            DateTime? expiryDate;
            if (!DateTime.TryParse(activationDateText, out activationDate))
            {
                expiryDate = null;
            }

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
            var extraInformation = GetCellPathValue(wbPart, wsPart, "B49").Trim();

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
                ProductCode = productCode,
                PromotionCode = promotionCode,
                CuponCode = cuponCode,
                TopicCode = topicCode,
                Description = description,
                Instruction = instruction,
                ExtraInformation = extraInformation,
                CountryId = 1, // hard code for japan
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            // check if any product with same barcode exists in db
            if(_repositoryManager.ProductRepository.Table.Any(m => m.BarCode == barCode))
            {
                Logger.Log(LogLevel.Error, string.Format("Barcode: {0} already exists", barCode));
                return null;
            }

            _repositoryManager.ProductRepository.Create(newProduct);
            _repositoryManager.ProductRepository.Save();

            return newProduct.ProductId;
        }
        
        static int GetBrandId(string brandName)
        {
            var brand = _repositoryManager.BrandRepository
                                          .Table
                                          .FirstOrDefault(m => m.Name == brandName);
            if(brand == null)
            {
                // create new brand
                brand = new Brand
                {
                    Name = brandName,
                    Enabled = true
                };

                _repositoryManager.BrandRepository.Create(brand);
                _repositoryManager.BrandRepository.Save();
            }

            return brand.BrandId;
        }

        static int? GetCategoryId(string topCateCode, string secondCateCode, string thirdCateCode)
        {
            var category = _repositoryManager.CategoryRepository
                                             .Table
                                             .Include(m => m.ParentCategory.ParentCategory)
                                             .FirstOrDefault(m => m.CategoryCode == thirdCateCode &&
                                                                  m.ParentCategory.CategoryCode == secondCateCode &&
                                                                  m.ParentCategory.ParentCategory.CategoryCode == topCateCode);
            if(category == null)
            {
                return null;
            }

            return category.CategoryId;
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
                            value = stringTable.SharedStringTable.
                              ElementAt(int.Parse(value)).InnerText;
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
    }
}
