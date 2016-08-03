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
        // private readonly ProductHelper _productHelper;
        // ctor
        public ProductsController()
        {
            _repositoryManager = new RepositoryManager();

        }

        // get new products
        [Route("New/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ProductSimpleModel>> Get(int? pageIndex = 1, int? pageSize = 10)
        {
            IList<Product> products;

            // get start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            // todo: get last 7 or 14 days?
            var from = DateTime.Today.AddDays(-14);

            products = await _repositoryManager.ProductRepository
                                                .Table
                                                .Include(m => m.Brand) // include brand table
                                                .Include(m => m.Images)
                                                .Where(m => (m.ActivationDate >= from &&
                                                            (m.ExpiryDate >= DateTime.Today || m.ExpiryDate == null)))
                                                .OrderByDescending(m => m.ActivationDate)
                                                .Skip(startIndex)
                                                .Take(pageSize.Value)
                                                .ToListAsync();

            // convert into simplified model
            return products.Select(m => m.ToSimpleModel());
        }

        // get product list by category
        [Route("Categories/{categoryId}/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetByCategory(int categoryId, int? pageIndex = 1, int? pageSize = 10)
        {
            // as we have 3 defined categories in gbmono
            // determine category level before retreiving products
            var category = _repositoryManager.CategoryRepository.Get(categoryId);

            if (category == null)
            {
                // todo: return empty product list
            }
            // start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            IList<Product> products = null;

            // top level
            if (category.ParentId == null)
            {
                products = await _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand)
                                                    .Include(m => m.Category.ParentCategory) // three level categories
                                                    .Include(m => m.Images) // include product images
                                                    .Where(m => m.Category.ParentCategory.ParentId == categoryId)
                                                    .ToListAsync();
                // return simplified models
                return products.Select(m => m.ToSimpleModel()).Skip(startIndex).Take(pageSize.Value);

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

                return products.Select(m => m.ToSimpleModel()).Skip(startIndex).Take(pageSize.Value);
            }

            // second level
            products = await _repositoryManager.ProductRepository
                                                .Table
                                                .Include(m => m.Brand)
                                                .Include(m => m.Category)
                                                .Include(m => m.Images) // include product images
                                                .Where(m => m.Category.ParentId == categoryId)
                                                .ToListAsync();

            return products.Select(m => m.ToSimpleModel()).Skip(startIndex).Take(pageSize.Value);
        }

        // get product list by brand
        [Route("Brands/{brandId}/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetByBrand(int brandId, int? pageIndex = 1, int? pageSize = 10)
        {
            // calculate start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            // second level
            var products = await _repositoryManager.ProductRepository
                                                   .Table
                                                   .Include(m => m.Brand)
                                                   .Include(m => m.Category)
                                                   .Include(m => m.Images) // include product images
                                                   .Where(m => m.BrandId == brandId)
                                                   .ToListAsync();

            return products.Select(m => m.ToSimpleModel()).Skip(startIndex).Take(pageSize.Value);
        }

        // get ranking product list
        [Route("Ranking")]
        public async Task<IEnumerable<ProductSimpleModel>> GetByRanking()
        {
            // todo: get products by ranking (views or scans)
            var products =  await _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand) // include brand table
                                                    .Include(m => m.Images)
                                                    .Where(m => (m.ActivationDate <= DateTime.Today &&
                                                                (m.ExpiryDate >= DateTime.Today || m.ExpiryDate == null)))
                                                    .OrderByDescending(m => m.CreatedDate)                                                
                                                    .Take(12)
                                                    .ToListAsync();
            // convert into simple product model
            return products.Select(m => m.ToSimpleModel());
        }

        // get single product by id
        public async Task<Product> GetById(int id)
        {
            // todo: check if the request is from browser or mobile app?

            // record product view event
            await Task.Run(() => CreateUserVisit(id, (short)UserVisitType.ProductView));



            // return detailed product model
            return await _repositoryManager.ProductRepository
                                           .Table
                                           .Include(m => m.Brand)
                                           .Include(m => m.Images)
                                           .Include(m => m.Category.ParentCategory.ParentCategory)
                                           .Include(m=>m.Tags)
                                           .SingleOrDefaultAsync(m => m.ProductId == id);
        }

        // get single product by barcode
        [Route("BarCodes/{code}")]
        public async Task<Product> GetByBarCode(string code)
        {
            // get product by barcode
            var product = await _repositoryManager.ProductRepository
                                           .Table
                                           .Include(m => m.Brand)
                                           .Include(m => m.Images)
                                           .Include(m => m.Category.ParentCategory.ParentCategory)
                                           .FirstOrDefaultAsync(m => m.BarCode == code);

            // record barcode scan event
            await Task.Run(() => CreateUserVisit(product.ProductId, (short)UserVisitType.ProductScan));

            // return detailed product
            return product;
        }

        //[Route("Search")]
        //public async Task<PagedResponse<ProductDoc>> Search(PagedRequest<ProductSearchRequest> req)
        //{
        //    return await Task.Run(() =>
        //    {
        //        return _productHelper.SearchByKeyword(req);
        //    });
        //}
        /// <summary>
        /// create product event record when product is accessed
        /// </summary>
        /// <param name="produtId"></param>
        /// <param name="eventTypeId"></param>
        private void CreateUserVisit(int produtId, short visitTypeId)
        {
            // get user name if user is authenticated
            var userName = User.Identity.IsAuthenticated ? User.Identity.Name : Const.UnAuthorizedUserId;

            var newUserVisit = new UserVisit
            {
                UserId = userName,
                VisitTypeId = visitTypeId,
                KeyId = produtId,
                Created = DateTime.Now
            };

            try
            {
                // create
                _repositoryManager.UserVisitRepository.Create(newUserVisit);
                _repositoryManager.UserVisitRepository.Save();

            }
            catch(Exception exp)
            {
                // get base excetpion
                var baseException = exp.GetBaseException();

                // logging
                Utils.Logger.log.Error("RequestUri:" + Request.RequestUri + " Error:" + baseException.Message + " /n Stack Trace:" + baseException.StackTrace);
            }

        }
    }
}
