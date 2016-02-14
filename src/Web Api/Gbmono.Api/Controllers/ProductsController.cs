using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Models;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;


        public ProductsController() 
        {
            _repositoryManager = new RepositoryManager();

        }
        
        // get product by id
        public async Task<ProductDetailModel> GetById(int id)
        {
            return await Task.Run(() =>
            {
                var product = _repositoryManager.ProductRepository.Table
                                                                  .Include(m => m.Country)
                                                                  .Include(m => m.Brand.Manufacturer)
                                                                  .Include(m => m.Retailers)
                                                                  .Include(m => m.WebShops)
                                                                  .SingleOrDefault(f => f.ProductId == id);
                if (product != null)
                {
                    var model = product.ToModel();
                    // model.Categories = _categoryService.GetProductCategoryList(product.CategoryId);
                    return model;
                }

                return null;
            });
        }

        public async Task<IEnumerable<ProductSimpleModel>> GetAll()
        {
            var productList  = await _repositoryManager.ProductRepository.Table
                                                       .Include(m => m.Brand)
                                                       .Include(m => m.Retailers)
                                                       .Take(20)
                                                       .ToListAsync();

            if (productList != null && productList.Count > 0)
            {
                var models = productList.Select(m => m.ToSimpleModel()).ToList();
                return models;
            }

            return null;

            // pls remove Task.Run method
            // use .ToListAsync() or GetAsync() method instead

            //return await Task.Run(() =>
            //{
            //    var productList = _repositoryManager.ProductRepository.Table
            //                                                    .Include(m => m.Brand)
            //                                                    .Include(m => m.Retailers)
            //                                                    .Take(20).ToList();
            //    if (productList != null && productList.Count > 0)
            //    {
            //        var models = productList.Select(m => m.ToSimpleModel()).ToList();
            //        return models;
            //    }
            //    return null;
            //});
        }

        [Route("Categories/{categoryId}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetByCategory(int categoryId)
        {
            return await Task.Run(() =>
            {
                var subCategories = _repositoryManager.CategoryRepository.Fetch(f => f.ParentId == categoryId).Select(s => s.CategoryId).ToList();
                var productList = _repositoryManager.ProductRepository.Table
                                    .Include(m => m.Brand)
                                    .Include(m => m.Retailers)
                                    .Where(m => subCategories.Contains(m.CategoryId))
                                    .OrderBy(m => m.PrimaryName)
                                    .Take(20)
                                    .ToList();
                if (productList != null && productList.Count > 0)
                {
                    var models = productList.Select(m => m.ToSimpleModel()).ToList();
                    return models;
                }
                return new List<ProductSimpleModel>();

            });
        }


        [Route("BarCodes/{code}")]
        public Product GetByBarCode(string code)
        {
            return _repositoryManager.ProductRepository
                                     .Table
                                     .SingleOrDefault(m => m.BarCode == code);
        }

        [Route("Recommends")]
        public IEnumerable<Product> GetRecommendedProducts()
        {
            return _repositoryManager.ProductRepository
                                     .Table
                                     .Include(m => m.Brand.Manufacturer) // 读取对应品牌和品牌商
                                     .OrderBy(m => m.ProductCode)
                                     .ToList();
                                     
        }

    }
}
