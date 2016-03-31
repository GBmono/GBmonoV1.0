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
using Gbmono.Api.Admin.HttpResults;

namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/Categories")]
    public class TagsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public TagsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        // get all tags
        public async Task<IEnumerable<Tag>> Get()
        {
            return await _repositoryManager.TagRepository.Table.OrderBy(m => m.Name).ToListAsync();
        }
    }
}
