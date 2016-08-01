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

        // get article by id
        public async Task<Article> GetById(int id)
        {
            return await _repositoryManager.ArticleRepository
                                                   .Table
                                                   .Include(m => m.Images)
                                                   .SingleOrDefaultAsync(m => m.ArticleId == id);
        }

        // get articles by date
        [Route("List/{articleTypeId}/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ArticleSimpleModel>> GetByType(short articleTypeId, int? pageIndex = 1, int? pageSize = 12)
        {
            // get start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            // load published articles by date
            var articles = await _repositoryManager.ArticleRepository
                                                   .Table
                                                   .Include(m => m.Images)
                                                   .Where(m => m.ArticleTypeId == articleTypeId && 
                                                               m.ModifiedDate < DbFunctions.AddDays(DateTime.Today, 1) &&
                                                               m.IsPublished == true)
                                                   .OrderByDescending(m => m.ModifiedDate)
                                                   .Skip(startIndex)
                                                   .Take(pageSize.Value)
                                                   .ToListAsync();

            // convert into binding models
            return articles.Select(m => m.ToSimpleToModel());
        }

        // get article by ranking 
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
