using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gbmono.Api.Extensions;
using Gbmono.Api.Models;
using Gbmono.EF.Infrastructure;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Temp")]
    public class TempController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        public TempController()
        {
            _repositoryManager = new RepositoryManager();
        }

        [Route("Shelf/{shelfId}/{pageIndex:int?}/{pageSize:int?}")]
        public List<ProductSimpleModel> GetProductByShelf(int shelfId, int? pageIndex = 1, int? pageSize = 10)
        {
            var brandId = shelfId;
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            var products = _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand)
                                                    .Include(m => m.Category.ParentCategory) // three level categories
                                                    .Include(m => m.Images) // include product images
                                                    .Where(m => m.BrandId == brandId)
                                                    .ToList();

            return products.Select(m => m.ToSimpleModel()).Skip(startIndex).Take(pageSize.Value).ToList();
        }

        [Route("Shop/{shopId}")]
        public IHttpActionResult GetShop(int shopId)
        {
            var shop = new TempShopModel();
            shop.name = "松本清东京";
            shop.address = "东京市XX大街XX号";
            shop.lat = 35.42;
            shop.lon = 139.46;
            shop.Images = new List<string>();
            shop.Images.Add("http://119.9.104.196/adminapi/Files/Shops/1.jpg");
            shop.Images.Add("http://119.9.104.196/adminapi/Files/Shops/2.jpg");
            shop.Images.Add("http://119.9.104.196/adminapi/Files/Shops/3.jpg");
            shop.Coupon = "http://119.9.104.196/adminapi/Files/Shops/4.jpg";

            return Ok(shop);
        }

        [Route("Debug")]
        [HttpGet]
        public IHttpActionResult Debug()
        {
            throw new NotImplementedException();
        }
    }

    public class TempShopModel
    {
        public string name { set; get; }

        public string address { set; get; }

        public double lat { set; get; }

        public double lon { set; get; }

        public List<string> Images { set; get; }

        public string Coupon { set; get; }
    }
}