using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using Gbmono.Search.IndexManager;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Builder
{
    public class ProductTagBuilder
    {
        private readonly RepositoryManager _repositoryManager;
        public ProductTagBuilder()
        {
            _repositoryManager = new RepositoryManager();
        }

        private NestClient<ProductTagDoc> Client
        {
            get
            {
                return new NestClient<ProductTagDoc>().SetIndex(Constants.IndexName.GbmonoV1_producttag).SetType(Constants.TypeName.ProductTag);
            }
        }

        public void DeleteIndex()
        {
            Console.WriteLine("Delete product tag index");
            Client.DeleteIndex();
        }

        public void CreateIndexMapping()
        {
            Client.CreateIndexWithAutoMapping();
        }

        public void Build()
        {
            var maxProductTagId = GetMaxProdcutTagId();
            Console.WriteLine("Start indexing about {0} product tag", maxProductTagId);

            int chunkSize = 500;
            var tasks = new List<Task>();
            var startIndex = 0;
            while (startIndex < maxProductTagId)
            {
                var productTagList = GetChunkProduct(startIndex, chunkSize);

                var docList = new List<ProductTagDoc>();
                foreach (var tag in productTagList)
                {
                    var doc = new ProductTagDoc();
                    doc.Id = tag.TagId;
                    doc.Name = tag.Name;
                    doc.TagTypeId = tag.TagTypeId;
                    docList.Add(doc);
                }
                if (docList.Count() > 0)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            Client.IndexDocuments(docList);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Console.WriteLine("#################### entry key to continue ###################");
                            Console.ReadLine();
                        }
                    }));
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
                Console.WriteLine("{0} product tag indexed", startIndex);
            }

            if (tasks.Count > 0)
            {
                Task.WaitAny(tasks.ToArray());
                var toRemove = tasks.Where(m => m.IsCompleted).ToArray();
                foreach (var t in toRemove)
                {
                    tasks.Remove(t);
                }
                Console.WriteLine("#");
            }
        }

        private int GetMaxProdcutTagId()
        {
            return _repositoryManager.TagRepository.Table.Max(m => m.TagId);
        }

        private List<Tag> GetChunkProduct(int index,int size)
        {
            return _repositoryManager.TagRepository.Table.ToList();
        }
    }
}
