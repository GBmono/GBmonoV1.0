using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gbmono.Crawler.AdapterInterface;
using Gbmono.EF.CrawlerModel;
using Gbmono.EF.DataContext;

namespace Gbmono.CrawlerProcessor.Controllers
{
    public class HomeController : Controller
    {
        private CrawlerAdapter _crawlerAdapter;

        public HomeController()
        {
            _crawlerAdapter = new CrawlerAdapter();
        }



        public ActionResult Index()
        {
            var task = Task.Run(() => _crawlerAdapter.GetAllCrawlInstanceName());

            var instances = task.Result.ToList();

            return View(instances);
        }

        public ActionResult WebsiteKeywordManager(int groupId, string url)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetWebsiteKeyword(int groupId,string url)
        {
            List<Website_KeywordType> result = new List<Website_KeywordType>();
            using (var context = new GbmonoCrawlerContext())
            {
                var website = context.WebsiteNames.SingleOrDefault(m => m.GroupId == groupId);
                if (website == null)
                {
                    website = new WebsiteName();
                    website.ThirdPartyUserId = -1;
                    website.SourceWebsite = url;
                    website.GroupId = groupId;
                    context.WebsiteNames.Add(website);
                    context.SaveChanges();
                }
                result = context.Website_KeywordTypes.Where(m => m.WebSiteId == website.Id).ToList();
            }
                
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}