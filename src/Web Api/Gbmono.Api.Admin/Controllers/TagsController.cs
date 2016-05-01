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
    [RoutePrefix("api/Tags")]
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

        // get tags by id
        [Route("Types/{typeId}")]
        public async Task<IEnumerable<Tag>> GetByType(int typeId)
        {
            return await _repositoryManager.TagRepository
                                           .Table
                                           .Where(m => m.TagTypeId == typeId)
                                           .OrderBy(m => m.Name)
                                           .ToListAsync();
        }

        // get by id
        public async Task<Tag> Get(int id)
        {
            return await _repositoryManager.TagRepository.GetAsync(id);
        }

        // create new tag
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Tag tag)
        {
            // todo: check if name exists

            _repositoryManager.TagRepository.Create(tag);
            await _repositoryManager.TagRepository.SaveAsync();

            return Ok();
        }
        
        // update tag
        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Tag tag)
        {
            _repositoryManager.TagRepository.Update(tag);
            await _repositoryManager.TagRepository.SaveAsync();

            return Ok();
        }

        // delete tag
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if(_repositoryManager.ProductTagRepository.Table.Any(m => m.TagId == id))
            {
                return new DataInvalidResult("Tag is being using.", Request);
            }

            _repositoryManager.TagRepository.Delete(id);
            await _repositoryManager.TagRepository.SaveAsync();

            return Ok();
        }
    }
}
