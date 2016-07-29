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
using Gbmono.Api.Models;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Brands")]
    public class BrandsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public BrandsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        public IEnumerable<Brand> GetAll()
        {
            return _repositoryManager.BrandRepository.Table.OrderBy(m => m.Name).ToList();
        }

        [Route("GroupByAlphabet")]
        public async Task<IEnumerable<BrandAlphabetGroup>> GetBrandsGroupByAlphabet()
        {
            // load all brands
            // convert into simple models
            var brandSimpleModels = await _repositoryManager.BrandRepository
                                                             .Table
                                                             .Select(m => new BrandSimpleModel
                                                             {
                                                                 BrandId = m.BrandId,
                                                                 Name = m.DisplayName,
                                                                 FirstAlphabet = m.FirstCharacter
                                                             })
                                                             .ToListAsync();
            // group by alphabet
            // get distinct letter
            var letters = brandSimpleModels.Select(m => m.FirstAlphabet).Distinct();

            var group = new List<BrandAlphabetGroup>();

            foreach(var letter in letters)
            {
                group.Add(new BrandAlphabetGroup
                {
                    Alphabet = letter,
                    Brands = brandSimpleModels.Where(m => m.FirstAlphabet == letter)
                });
            }

            return group.OrderBy(m => m.Alphabet);
        }

        [HttpGet]
        public Brand GetById(int id)
        {
            return _repositoryManager.BrandRepository.Get(id);
        }
    }
}
