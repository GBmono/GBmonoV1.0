using Gbmono.EF.Infrastructure;
using Gbmono.Search.IndexManager;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Builder
{
    public class ProductBuilder
    {
        private readonly RepositoryManager _repositoryManager;
        public ProductBuilder()
        {
            _repositoryManager = new RepositoryManager();
        }

        private NestClient<ProductDoc> Client
        {
            get
            {
                return new NestClient<ProductDoc>().SetIndex(Constants.IndexName.GbmonoV1).SetType(Constants.TypeName.Product);
            }
        }

        public void CreateIndexMapping()
        {
            Client.CreateIndexWithAutoMapping();
        }

        public void Build()
        {

        }
    }
}
