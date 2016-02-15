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
                                     .Include(m => m.Country)
                                     .Where(m => m.CategoryId == categoryId)                                     
                                     .Select(m => new ProductSimpleModel
                                     {
                                         ProductId = m.ProductId,
                                         ProductCode = m.ProductCode,
                                         CategoryId = m.CategoryId,
                                         BrandId = m.BrandId,
                                         BrandName = m.Brand.Name,
                                         CountryId = m.CountryId,
                                         CountryName = m.Country.Name,
                                         PrimaryName = m.PrimaryName,
                                         SecondaryName = m.SecondaryName,
                                         BarCode = m.BarCode,
                                         Price = m.Price,
                                         Discount = m.Discount,
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
