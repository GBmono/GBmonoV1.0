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
            return await _repositoryManager.ProductRepository
                                     .Table
                                     .Include(m => m.Brand)
                                     // .Include(m => m.Country)
                                     .Include(m => m.Images)
                                     .Where(m => m.CategoryId == categoryId)                                     
                                     .Select(m => new ProductSimpleModel
                                     {
                                         ProductId = m.ProductId,
                                         ProductCode = m.ProductCode,
                                         // CategoryId = m.CategoryId,
                                         BrandId = m.BrandId,
                                         BrandName = m.Brand.Name,
                                         // CountryId = m.CountryId,
                                         // CountryName = m.Country.Name,
                                         PrimaryName = m.PrimaryName,
                                         // SecondaryName = m.SecondaryName,
                                         BarCode = m.BarCode,
                                         Price = m.Price,
                                         Discount = m.Discount,
                                         ImgUrl = m.Images.FirstOrDefault(s => s.ProductImageTypeId == (short) ProductImageType.Product) == null
                                                  ? ""
                                                  : m.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product).FileName,
                                         ActivationDate = m.ActivationDate,
                                         ExpiryDate = m.ExpiryDate
                                     }).ToListAsync();
        }

        [Route("Search")]
        public async Task<IEnumerable<ProductSimpleModel>> Search([FromBody] ProductSearchModel model)
        {
            // barcode 
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                return await _repositoryManager.ProductRepository
                         .Table
                         .Include(m => m.Brand)
                         .Include(m => m.Images)
                         .Where(m => m.BarCode == model.BarCode)
                         .Select(m => new ProductSimpleModel
                         {
                             ProductId = m.ProductId,
                             ProductCode = m.ProductCode,
                                         BrandId = m.BrandId,
                             BrandName = m.Brand.Name,
                                         PrimaryName = m.PrimaryName,
                                         BarCode = m.BarCode,
                             Price = m.Price,
                             Discount = m.Discount,
                             ImgUrl = m.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product) == null
                                      ? ""
                                      : m.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product).FileName,
                             ActivationDate = m.ActivationDate,
                             ExpiryDate = m.ExpiryDate
                         }).ToListAsync();
            }

            // invalid product code
            if (!Validator.IsValidFullProductCode(model.FullProductCode))
            {
                // return emty colleciton
                return new List<ProductSimpleModel>();
            }

            // full product code
            // extract category code from full product code
            var topCateCode = model.FullProductCode.Substring(0, 2);
            var secondCateCode = model.FullProductCode.Substring(2, 2);
            var thirdCateCode = model.FullProductCode.Substring(4, 2);
            var productCode = model.FullProductCode.Substring(6, 4);

            return await _repositoryManager.ProductRepository
                                            .Table
                                            .Include(m => m.Brand)
                                            .Include(m => m.Images)
                                            .Include(m => m.Category.ParentCategory.ParentCategory) // include all three level categories
                                            .Where(m => m.ProductCode == productCode &&
                                                        m.Category.CategoryCode == thirdCateCode &&
                                                        m.Category.ParentCategory.CategoryCode == secondCateCode &&
                                                        m.Category.ParentCategory.ParentCategory.CategoryCode == topCateCode)
                                            .Select(m => new ProductSimpleModel
                                            {
                                                ProductId = m.ProductId,
                                                ProductCode = m.ProductCode,
                                                BrandId = m.BrandId,
                                                BrandName = m.Brand.Name,
                                                PrimaryName = m.PrimaryName,
                                                BarCode = m.BarCode,
                                                Price = m.Price,
                                                Discount = m.Discount,
                                                ImgUrl = m.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product) == null
                                                        ? ""
                                                        : m.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product).FileName,
                                                ActivationDate = m.ActivationDate,
                                                ExpiryDate = m.ExpiryDate
                                            }).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _repositoryManager.ProductRepository
                                           .Table
                                           .Include(m => m.Category.ParentCategory)
                                           .SingleOrDefaultAsync(m => m.ProductId == id);
        }

        [Route("CountByTopCategory")]
        // return product count by top category
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

        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]Product product)
        {
            product.UpdatedDate = DateTime.Now;

            _repositoryManager.ProductRepository.Update(product);
            _repositoryManager.ProductRepository.Save();

            return Ok(product.ProductId);
        }
    }
}
