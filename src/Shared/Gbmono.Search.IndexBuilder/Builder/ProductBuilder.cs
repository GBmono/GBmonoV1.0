using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using Gbmono.Search.IndexManager;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Builder
{
    public class ProductBuilder
    {
        private readonly RepositoryManager _repositoryManager;
        public ProductBuilder()
        {
            _repositoryManager = new RepositoryManager();
        }

        private NestClient<ProductDoc> Client
        {
            get
            {
                return new NestClient<ProductDoc>().SetIndex(Constants.IndexName.GbmonoV1_product).SetType(Constants.TypeName.Product);
            }
        }

        public void DeleteIndex()
        {
            Console.WriteLine("Delete product index");
            Client.DeleteIndex();
        }

        public void CreateIndexMapping()
        {
            Client.CreateIndexWithAutoMapping();
        }

        public void Build()
        {
            var maxProductId = GetMaxProductId();

            Console.WriteLine("Start indexing about {0} product", maxProductId);

            int chunkSize = 500;
            var tasks = new List<Task>();
            var startIndex = 0;
            while (startIndex < maxProductId)
            {
                var productList = GetChunkProduct(startIndex, chunkSize);

                var docList = new List<ProductDoc>();
                foreach (var product in productList)
                {
                    var doc = new ProductDoc();
                    doc.ProductId = product.ProductId;
                    var categories = GetParentCategories(product.CategoryId);
                    for (int i = 0; i < categories.Count(); i++)
                    {                        
                        switch (i)
                        {
                            case 1:
                                doc.CategoryLevel1 = categories[i];
                                break;
                            case 2:
                                doc.CategoryLevel2 = categories[i];
                                break;
                            default:
                                doc.CategoryLevel3 = categories[i];
                                break;
                        }
                    }
                    doc.BrandId = product.BrandId;
                    doc.BrandName = product.Brand.Name;
                    doc.BrandCollectionId = product.BrandCollectionId;
                    doc.BrandCollectionName = product.BrandCollectionName;
                    doc.ProductCode = product.ProductCode;
                    doc.Barcode = product.BarCode;
                    doc.Name = product.PrimaryName;
                    doc.Name_NA = doc.Name;
                    doc.AlternativeName = product.SecondaryName;
                    doc.PromotionCode = product.PromotionCode;
                    doc.CuponCode = product.CuponCode;
                    doc.TopicCode = product.TopicCode;
                    doc.RankingCode = product.RankingCode;
                    doc.Capacity = product.Capacity;
                    doc.Weight = product.Weight;
                    doc.Flavor = product.Flavor;

                    doc.Width = product.Width;
                    doc.Height = product.Height;
                    doc.Depth = product.Depth;
                    doc.Price = product.Price;
                    doc.Spring = product.Spring;
                    doc.Summer = product.Summer;
                    doc.Autumn = product.Autumn;
                    doc.Winter = product.Winter;
                    doc.Discount = product.Discount;
                    doc.Description = product.Description;
                    doc.Instruction = product.Instruction;
                    doc.ExtraInformation = product.ExtraInformation;
                    doc.UpdatedDate = product.UpdatedDate;
                    doc.ActivationDate = product.ActivationDate;
                    doc.ExpiryDate = product.ExpiryDate;
                    //doc.Tags = string.Join(" ", GetProductTags(product.ProductId));
                    doc.Tags = GetProductTags(product.ProductId);
                    if (product.Images != null && product.Images.Count > 0)
                    {
                        doc.Images = new List<ProductImageDoc>();
                    }
                    foreach (var image in product.Images)
                    {
                        var imageDoc = new ProductImageDoc();
                        imageDoc.ProductImageId = image.ProductImageId;
                        imageDoc.Name = image.Name;
                        imageDoc.FileName = image.FileName;
                        imageDoc.ProductId = image.ProductId;
                        imageDoc.IsPrimary = image.IsPrimary;
                        imageDoc.IsThumbnail = image.IsThumbnail;
                        imageDoc.ProductImageTypeId = image.ProductImageTypeId;
                        doc.Images.Add(imageDoc);
                    }
                    docList.Add(doc);
                }
                if (docList.Count() > 0)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            Client.IndexDocuments(docList);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Console.WriteLine("#################### entry key to continue ###################");
                            Console.ReadLine();
                        }
                    }));
                }
                if (tasks.Count > 3)
                {
                    Task.WaitAny(tasks.ToArray());
                    var toRemove = tasks.Where(m => m.IsCompleted).ToArray();
                    foreach (var t in toRemove)
                    {
                        tasks.Remove(t);
                    }
                    Console.WriteLine("#");
                }

                startIndex += chunkSize;
                Console.WriteLine("{0} product indexed", startIndex);
            }
            if (tasks.Count > 0)
            {
                Task.WaitAny(tasks.ToArray());
                var toRemove = tasks.Where(m => m.IsCompleted).ToArray();
                foreach (var t in toRemove)
                {
                    tasks.Remove(t);
                }
                Console.WriteLine("#");
            }
        }

        private int GetMaxProductId()
        {
            return _repositoryManager.ProductRepository.Table.Max(m => m.ProductId);
        }

        private List<Product> GetChunkProduct(int index, int size)
        {
            return _repositoryManager.ProductRepository.Table.Include(m=>m.Images).Include(m=>m.Brand).Where(m => m.ProductId >= index && m.ProductId < index + size).ToList();
        }

        private List<string> GetProductTags(int productId)
        {
            return _repositoryManager.ProductTagRepository.Table.Include(t => t.Tag).Where(m => m.ProductId == productId).Select(m => m.Tag.Name).ToList();
        }

        private List<string> GetParentCategories(int categoryId)
        {
            var categoryList = new List<string>();
            
            var category = _repositoryManager.CategoryRepository.Table.FirstOrDefault(m => m.CategoryId == categoryId);
            categoryList.Add(category.Name);
            // get level 2 parent category
            if (category.ParentId != null)
            {                
                var parentCategory = _repositoryManager.CategoryRepository.Table.FirstOrDefault(m => m.CategoryId == category.ParentId);
                categoryList.Add(parentCategory.Name);
                // get level 1 parent category
                if (parentCategory.ParentId != null)
                {
                    var rootCategory = _repositoryManager.CategoryRepository.Table.FirstOrDefault(m => m.CategoryId == parentCategory.ParentId);
                    categoryList.Add(rootCategory.Name);
                }
            }
            //return string.Join(" ", categoryList);
            categoryList.Reverse();
            return categoryList;
        }
    }
}
