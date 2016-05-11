using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
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
    public class BrandBuilder
    {
        private readonly RepositoryManager _repositoryManager;
        public BrandBuilder()
        {
            _repositoryManager = new RepositoryManager();
        }      
        
        private NestClient<BrandDoc> Client
        {
            get
            {
                return new NestClient<BrandDoc>().SetIndex(Constants.IndexName.GbmonoV1).SetType(Constants.TypeName.Brand);
            }
        }

        public void CreateIndexMapping()
        {
            Client.CreateIndexWithAutoMapping();
        }

        public void Build()
        {
            var maxBrandId = GetMaxBrandId();
            Console.WriteLine("Start indexing {0} Retail Shops", maxBrandId);

            int chunkSize = 1000;
            var tasks = new List<Task>();
            var startIndex = 0;
            while (startIndex < maxBrandId)
            {
                var brandList = GetChunkBrand(startIndex, chunkSize);

                var docList = new List<BrandDoc>();
                foreach (var b in brandList)
                {
                    var doc = new BrandDoc()
                    {
                        BrandId = b.BrandId,
                        Name = b.Name,
                        DisplayName = b.DisplayName,
                        BrandCode = b.BrandCode,
                        FirstCharacter = b.FirstCharacter,
                        LogoUrl = b.LogoUrl,
                        Description = b.Description,
                        Enabled = b.Enabled
                    };
                    docList.Add(doc);
                }

                try
                {
                    Client.IndexDocuments(docList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Brand Builder index documents error :{0}", ex);
                }

                if (tasks.Count > 3)
                {
                    Task.WaitAny(tasks.ToArray());
                    var toRemove = tasks.Where(m => m.IsCompleted).ToArray();
                    foreach (var t in toRemove)
                    {
                        tasks.Remove(t);
                    }
                    Console.WriteLine("#");
                }

                startIndex += chunkSize;
                Console.WriteLine("{0} Brand indexed", startIndex);
            }
        }

        private int GetMaxBrandId()
        {
            return _repositoryManager.BrandRepository.Table.Max(m => m.BrandId);
        }

        private List<Brand> GetChunkBrand(int index, int size)
        {
            return _repositoryManager.BrandRepository.Table.Where(m => m.BrandId >= index && m.BrandId < index + size).ToList();
        }
    }
}
