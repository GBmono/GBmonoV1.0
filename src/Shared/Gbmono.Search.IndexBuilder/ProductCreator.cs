
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;

namespace Gbmono.Search.IndexBuilder
{
    public class ProductCreator
    {
        private readonly RepositoryManager _repositoryManager;

        public ProductCreator()
        {
            _repositoryManager = new RepositoryManager();
        }

        private List<Product> GetProducts()
        {
            return _repositoryManager.ProductRepository.Table.ToList();
        }
    }
}
