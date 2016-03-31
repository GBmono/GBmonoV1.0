using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.Models;
using Gbmono.Api.Admin.Extensions;
using Gbmono.Common;


namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public ProductsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        [Route("Categories/{categoryId}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetByCategory(int categoryId)
        {
            var products =  await _repositoryManager.ProductRepository
                                     .Table
                                     .Include(m => m.Brand)
                                     // .Include(m => m.Country)
                                     .Include(m => m.Images)
                                     .Where(m => m.CategoryId == categoryId)                                     
                                     .ToListAsync();

            return products.Select(m => m.ToSimpleModel());
        }

        [Route("Search")]
        public async Task<IEnumerable<ProductSimpleModel>> Search([FromBody] ProductSearchModel model)
        {
            // barcode 
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                var productsByBarcode = await _repositoryManager.ProductRepository
                                                       .Table
                                                       .Include(m => m.Brand)
                                                       .Include(m => m.Images)
                                                       .Where(m => m.BarCode == model.BarCode)
                                                       .ToListAsync();
                // convert into binding model
                return productsByBarcode.Select(m => m.ToSimpleModel());
            }

            // invalid product code
            if (!Validator.IsValidFullProductCode(model.FullProductCode))
            {
                // return last 30 new products
                var newProducts = await _repositoryManager.ProductRepository
                                            .Table
                                            .Include(m => m.Brand)
                                            .Include(m => m.Images)
                                            .Include(m => m.Category.ParentCategory.ParentCategory) // include all three level categories
                                            .OrderByDescending(m => m.CreatedDate)
                                            .Take(30)
                                            .ToListAsync();

                // convert into binding model
                return newProducts.Select(m => m.ToSimpleModel());                                        
            }

            // full product code
            // extract category code from full product code
            var topCateCode = model.FullProductCode.Substring(0, 2);
            var secondCateCode = model.FullProductCode.Substring(2, 2);
            var thirdCateCode = model.FullProductCode.Substring(4, 2);
            var productCode = model.FullProductCode.Substring(6, 4);

            var productsByCode =  await _repositoryManager.ProductRepository
                                            .Table
                                            .Include(m => m.Brand)
                                            .Include(m => m.Images)
                                            .Include(m => m.Category.ParentCategory.ParentCategory) // include all three level categories
                                            .Where(m => m.ProductCode == productCode &&
                                                        m.Category.CategoryCode == thirdCateCode &&
                                                        m.Category.ParentCategory.CategoryCode == secondCateCode &&
                                                        m.Category.ParentCategory.ParentCategory.CategoryCode == topCateCode)
                                            .ToListAsync();

            // convert into binding model
            return productsByCode.Select(m => m.ToSimpleModel());
        }

        public async Task<Product> GetById(int id)
        {
            return await _repositoryManager.ProductRepository
                                           .Table
                                           .Include(m => m.Category.ParentCategory)
                                           .SingleOrDefaultAsync(m => m.ProductId == id);
        }

        // return tags by product
        [Route("{id}/Tags")]
        public async Task<IEnumerable<Tag>> GetTags(int id)
        {
            // get product tags
            var tags = await _repositoryManager.ProductTagRepository
                                               .Table
                                               .Include(m => m.Tag)
                                               .Where(m => m.ProductId == id)
                                               .Select(m => m.Tag)
                                               .ToListAsync();

            return tags;
        }

        // return product count by top category
        [Route("CountByTopCategory")]        
        public async Task<IEnumerable<KendoBarChartItem>> GetProductCount()
        {
            // get top categories
            var categories = await _repositoryManager.CategoryRepository
                                                     .Table
                                                     .Where(m => m.ParentId == null)
                                                     .OrderBy(m => m.CategoryCode)
                                                     .ToListAsync();

            // return model
            var resultset = new List<KendoBarChartItem>();

            // count the product by each top category
            foreach(var cate in categories)
            {
                var productCount = _repositoryManager.ProductRepository
                                                     .Table
                                                     .Count(m => m.Category.ParentCategory.ParentId == cate.CategoryId);

                resultset.Add(new KendoBarChartItem { Category = cate.Name, ProductCount = productCount });
            }

            // return
            return resultset.OrderByDescending(m => m.ProductCount);
        }

        // create product
        [HttpPost]
        public IHttpActionResult Create([FromBody]Product product)
        {
            // todo: validation

            // update create datetime and update datetime
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;

            _repositoryManager.ProductRepository.Create(product);
            _repositoryManager.ProductRepository.Save();

            return Ok(product.ProductId);
        }

        // update product
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]Product product)
        {
            product.UpdatedDate = DateTime.Now;

            _repositoryManager.ProductRepository.Update(product);
            _repositoryManager.ProductRepository.Save();

            return Ok(product.ProductId);
        }

        // save tags
        [HttpPost]
        [Route("SaveTags")]
        public async Task<IHttpActionResult> Save([FromBody] ProductTagSaveModel model)
        {
            // load existed product tags
            var productTags = await _repositoryManager.ProductTagRepository
                                                      .Table
                                                      .Where(m => m.ProductId == model.ProductId)
                                                      .ToListAsync();

            // remove tags not in the save mode list
            foreach (var productTag in productTags)
            {
                if (!model.TagIds.Any(m => m == productTag.TagId))
                {
                    _repositoryManager.ProductTagRepository.Delete(productTag);
                }
            }

            // create tag 
            foreach (var tagId in model.TagIds)
            {
                // create if desn't exist
                if (!productTags.Any(m => m.TagId == tagId))
                {
                    // create
                    _repositoryManager.ProductTagRepository
                                      .Create(new ProductTag { ProductId = model.ProductId, TagId = tagId });
                }
            }

            // save changes
            await _repositoryManager.ProductTagRepository.SaveAsync();
            return Ok();

        }
    }
}
