using Gbmono.Models;
using Gbmono.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
