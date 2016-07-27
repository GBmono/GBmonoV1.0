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
    [RoutePrefix("api/Articles")]
    public class ArticlesController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        // ctor
        public ArticlesController()
        {
            _repositoryManager = new RepositoryManager();

        }

        public async Task<Article> GetById(int id)
        {
            return await _repositoryManager.ArticleRepository
                                                   .Table
                                                   .Include(m => m.Images)
                                                   .SingleOrDefaultAsync(m => m.ArticleId == id);
        }

        [Route("{from}/{to}")]
        public async Task<IEnumerable<ArticleSimpleModel>> GetArticles(DateTime from, DateTime to)
        {
            // todo: limitation on date range?
            if((to - from).Days > 60)
            {
                // todo:
            }

            // load published articles by date
            var articles = await _repositoryManager.ArticleRepository
                                                   .Table
                                                   .Include(m => m.Images)
                                                   .Where(m => m.ModifiedDate >= from &&
                                                               m.ModifiedDate < DbFunctions.AddDays(to, 1) &&
                                                               m.IsPublished == true)
                                                   .OrderByDescending(m => m.ModifiedDate)
                                                   .ToListAsync();

            // convert into binding models
            return articles.Select(m => m.ToSimpleToModel());
        }

        [Route("Ranking")]
        public async Task<IEnumerable<ArticleSimpleModel>> GetArticlesByRanking()
        {
            // todo: get the articles by ranking
            // load published articles by date
            var articles = await _repositoryManager.ArticleRepository
                                                   .Table
                                                   .Include(m => m.Images)
                                                   .Where(m => m.IsPublished == true)
                                                   .OrderByDescending(m => m.ModifiedDate)
                                                   .Take(12)
                                                   .ToListAsync();

            // convert into binding models
            return articles.Select(m => m.ToSimpleToModel());
        }
    }
}
