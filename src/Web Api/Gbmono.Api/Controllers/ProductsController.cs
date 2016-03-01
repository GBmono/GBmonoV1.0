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
using Gbmono.Api.Extensions;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        // ctor
        public ProductsController() 
        {
            _repositoryManager = new RepositoryManager();

        }


        [Route("New/{pageIndex:int?}/{pageSize:int?}")] 
        public async Task<IEnumerable<ProductSimpleModel>> Get(int? pageIndex = 1, int? pageSize = 10)
        {
            IList<Product> products;
            // first page
            if(pageIndex == 1)
            {
                // return first pagesize of products
                products = await _repositoryManager.ProductRepository
                                                   .Table
                                                   .Include(m => m.Brand) // include brand table
                                                   .Include(m => m.Images)
                                                   .Where(m => (m.ActivationDate <= DateTime.Today &&
                                                               (m.ExpiryDate >= DateTime.Today || m.ExpiryDate == null)))
                                                   .OrderByDescending(m => m.ActivationDate)
                                                   .Take(pageSize.Value)
                                                   .ToListAsync();

                // convert into simplified model
                return products.Select(m => m.ToSimpleModel());
            }

            // if page index is lager than 1
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            products = await _repositoryManager.ProductRepository
                                                .Table
                                                .Include(m => m.Brand) // include brand table
                                                .Include(m => m.Images)
                                                .Where(m => (m.ActivationDate <= DateTime.Today &&
                                                            (m.ExpiryDate >= DateTime.Today || m.ExpiryDate == null)))
                                                .OrderByDescending(m => m.ActivationDate)
                                                .Skip(startIndex)
                                                .Take(pageSize.Value)
                                                .ToListAsync();

            // convert into simplified model
            return products.Select(m => m.ToSimpleModel());                                    
        }

        [Route("Categories/{categoryId}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetByCategory(int categoryId)
        {
            // as we have 3 defined categories in gbmono
            // determine category level before retreiving products
            var category = _repositoryManager.CategoryRepository.Get(categoryId);

            if (category == null)
            {
                // todo: return empty product list
            }
            IList<Product> products = null;

            // top level
            if (category.ParentId == null)
            {
                products =  await _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand)
                                                    .Include(m => m.Category.ParentCategory) // three level categories
                                                    .Include(m => m.Images) // include product images
                                                    .Where(m => m.Category.ParentCategory.ParentId == categoryId)
                                                    .ToListAsync();
                // return simplified models
                return products.Select(m => m.ToSimpleModel());
                                               
            }

            // third level
            // when the current category id is a non-parent id
            if (!_repositoryManager.CategoryRepository.Table.Any(m => m.ParentId == categoryId))
            {
                products = await _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand)
                                                    .Include(m => m.Images) // include product images
                                                    .Where(m => m.CategoryId == categoryId)                                               
                                                    .ToListAsync();

                return products.Select(m => m.ToSimpleModel());
            }

            // second level
            products = await _repositoryManager.ProductRepository
                                                .Table
                                                .Include(m => m.Brand)
                                                .Include(m => m.Category)
                                                .Include(m => m.Images) // include product images
                                                .Where(m => m.Category.ParentId == categoryId)
                                                .ToListAsync();

            return products.Select(m => m.ToSimpleModel());
        }

        // get by product id, return detailed product model
        public async Task<Product> GetById(int id)
        {
            // todo: check if the request is from browser or mobile app

            return await _repositoryManager.ProductRepository
                                           .Table
                                           .Include(m => m.Brand)
                                           .Include(m => m.Images)
                                           .Include(m => m.Category.ParentCategory.ParentCategory)
                                           .SingleOrDefaultAsync(m => m.ProductId == id);
        }

        [Route("BarCodes/{code}")]
        public Product GetByBarCode(string code)
        {
            return _repositoryManager.ProductRepository
                                     .Table
                                     .SingleOrDefault(m => m.BarCode == code);
        }

    }
}
