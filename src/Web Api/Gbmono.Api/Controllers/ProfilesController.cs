using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Models;
using Gbmono.Api.Security.Identities;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Profiles")]
    [Authorize]
    public class ProfilesController : ApiController
    {
        private readonly GbmonoUserManager _userManager = null;
        private readonly RepositoryManager _repositoryManager;


        public ProfilesController()
        {
            _repositoryManager = new RepositoryManager();
        }

        public GbmonoUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<GbmonoUserManager>(); }
        }


        [HttpPut]
        public async Task<IHttpActionResult> Update(GbmonoUser user)
        {
            var userInfo = await UserManager.FindByNameAsync(RequestContext.Principal.Identity.Name);
            userInfo.DisplayName = user.DisplayName;
            userInfo.PhoneNumber = user.PhoneNumber;
            UserManager.Update(userInfo);
            return Ok(userInfo);
        }


        // [HttpGet]
        // [Route("GetFollowProducts")]
        //public async Task<IHttpActionResult> GetFollowProducts()
        //{
        //    var userId = RequestContext.Principal.Identity.GetUserId();
        //    var followProductIds = _repositoryManager.FollowOptionRepository.Fetch(m => m.UserId == userId && m.FollowTypeId == (int)FollowOptionType.FollowProduct).Select(m => m.OptionId);
        //    var result = new List<Product>();
        //    if (followProductIds.Any())
        //    {
        //        result = _repositoryManager.ProductRepository.Table.Include(m => m.Retailers).Where(m => followProductIds.Contains(m.ProductId)).ToList();

        //        //Todo Temp Add Image
        //        foreach (var productCollection in result)
        //        {
        //            if (productCollection.Images == null)
        //            {
        //                productCollection.Images = new List<ProductImage>();
        //                productCollection.Images.Add(new ProductImage() { IsPrimary = true, IsThumbnail = false, Name = "PicTemp", Url = "/content/images/demo/merries2_f.jpg" });
        //                productCollection.Images.Add(new ProductImage()
        //                {
        //                    IsPrimary = false,
        //                    IsThumbnail = false,
        //                    Name = "PicTemp2",
        //                    Url = "/content/images/demo/merries2_b.jpg"
        //                });
        //            }
        //        }
        //    }
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("GetFavoriteProducts")]
        //public async Task<IHttpActionResult> GetFavoriteProducts()
        //{
        //    var userId = RequestContext.Principal.Identity.GetUserId();
        //    var favoriteProductIds = _repositoryManager.FollowOptionRepository.Fetch(m => m.UserId == userId && m.FollowTypeId == (int)FollowOptionType.FavoriteProduct).Select(m => m.OptionId);
        //    var result = new List<Product>();
        //    if (favoriteProductIds.Any())
        //    {
        //        result = _repositoryManager.ProductRepository.Table.Include(m=>m.Retailers).Where(m => favoriteProductIds.Contains(m.ProductId)).ToList();

        //        //Todo Temp Add Image
        //        foreach (var productCollection in result)
        //        {
        //            if (productCollection.Images == null)
        //            {
        //                productCollection.Images = new List<ProductImage>();
        //                productCollection.Images.Add(new ProductImage() { IsPrimary = true, IsThumbnail = false, Name = "PicTemp", Url = "/content/images/demo/merries2_f.jpg" });
        //                productCollection.Images.Add(new ProductImage()
        //                {
        //                    IsPrimary = false,
        //                    IsThumbnail = false,
        //                    Name = "PicTemp2",
        //                    Url = "/content/images/demo/merries2_b.jpg"
        //                });
        //            }
        //        }
        //    }
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("GetFollowBrands")]
        //public async Task<IHttpActionResult> GetFollowBrands()
        //{
        //    var userId = RequestContext.Principal.Identity.GetUserId();
        //    var brandIds = _repositoryManager.FollowOptionRepository.Fetch(m => m.UserId == userId && m.FollowTypeId == (int)FollowOptionType.FollowBrand).Select(m => m.OptionId);
        //    var result = new List<Brand>();
        //    if (brandIds.Any())
        //    {
        //        result = _repositoryManager.BrandRepository.Fetch(m => brandIds.Contains(m.BrandId)).ToList();
        //    }
        //    return Ok(result);
        //}


      

    }
}
