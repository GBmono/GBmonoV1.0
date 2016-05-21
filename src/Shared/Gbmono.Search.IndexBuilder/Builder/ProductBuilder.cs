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
            var maxProductId = GetMaxProductShipId();

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
                    //var doc = new ProductDoc
                    //{
                    //    ProductId = product.ProductId,
                    //    Categories = GetParentCategories(product.CategoryId),
                    //    BrandId = product.BrandId,
                    //    //BrandCollectionId = product.BrandCollectionId.HasValue ? product.BrandCollectionId.Value : 0,
                    //    BrandCollectionId=product.BrandCollectionId,
                    //    ProductCode = product.ProductCode,
                    //    Barcode = product.BarCode,
                    //    Name = product.SecondaryName,
                    //    PromotionCode = product.PromotionCode,
                    //    CuponCode = product.CuponCode,
                    //    TopicCode = product.TopicCode,
                    //    RankingCode = product.RankingCode,
                    //    Capacity = product.Capacity,
                    //    Weight = product.Weight,
                    //    Flavor = product.Flavor,
                    //    //Width = product.Width.HasValue ? product.Width.Value : 0,
                    //    //Height = product.Height.HasValue ? product.Height.Value:0,
                    //    //Depth=product.Depth.HasValue?product.Depth.Value:0,
                    //    Width=product.Width,
                    //    Height=product.Height,
                    //    Depth=product.Depth,
                    //    Price=product.Price,
                    //    Spring=product.Spring,
                    //    Summer=product.Summer,
                    //    Autumn=product.Autumn,
                    //    Winter=product.Winter,
                    //    Discount=product.Discount,
                    //    Description=product.Description,
                    //    Instruction=product.Instruction,
                    //    ExtraInformation=product.ExtraInformation,
                    //    UpdatedDate=product.UpdatedDate,
                    //    ActivationDate=product.ActivationDate,
                    //    ExpiryDate=product.ExpiryDate,
                    //    Tags=string.Join(" ",product.Tags.Select(s=>s.TagId).ToList())
                    //};
                    var doc = new ProductDoc();
                    doc.ProductId = product.ProductId;
                    doc.Categories = GetParentCategories(product.CategoryId);
                    doc.BrandId = product.BrandId;
                    doc.BrandCollectionId = product.BrandCollectionId;
                    doc.ProductCode = product.ProductCode;
                    doc.Barcode = product.BarCode;
                    doc.Name = product.SecondaryName;
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
                    doc.Tags = string.Join(" ", GetProductTags(product.ProductId));
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

                //tasks.Add(Task.Run(() =>
                //{
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
                //}));

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
        }

        private int GetMaxProductShipId()
        {
            return _repositoryManager.ProductRepository.Table.Max(m => m.ProductId);
        }

        private List<Product> GetChunkProduct(int index, int size)
        {
            return _repositoryManager.ProductRepository.Table.Include(m=>m.Images).Where(m => m.ProductId >= index && m.ProductId < index + size).ToList();
        }

        private List<int> GetProductTags(int productId)
        {
            return _repositoryManager.ProductTagRepository.Table.Where(m => m.ProductId == productId).Select(s=>s.TagId).ToList();
        }

        private string GetParentCategories(int categoryId)
        {
            var categoryList = new List<int> { categoryId };
            
            var category = _repositoryManager.CategoryRepository.Table.FirstOrDefault(m => m.CategoryId == categoryId);

            // get level 2 parent category
            if (category.ParentId != null)
            {                
                var parentCategory = _repositoryManager.CategoryRepository.Table.FirstOrDefault(m => m.CategoryId == category.ParentId);
                categoryList.Add(parentCategory.CategoryId);
                // get level 1 parent category
                if (parentCategory.ParentId != null)
                {
                    categoryList.Add(parentCategory.ParentId.Value);
                }
            }
            return string.Join(" ", categoryList);
        }
    }
}
