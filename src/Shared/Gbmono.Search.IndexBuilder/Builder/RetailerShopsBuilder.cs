using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using Gbmono.Search.IndexManager;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Builder
{
    public class RetailerShopsBuilder
    {
        private readonly RepositoryManager _repositoryManager;
        public RetailerShopsBuilder()
        {
            _repositoryManager = new RepositoryManager();
        }
        private NestClient<RetailShopDoc> Client
        {
            get
            {
                return new NestClient<RetailShopDoc>().SetIndex(Constants.IndexName.GbmonoV1).SetType(Constants.TypeName.RetailShop);
            }
        }

        public void CreateIndexMapping()
        {
            Client.CreateIndexWithMappingForRetailShop();
        }

        public void Build()
        {
            var maxRetailShopId = GetMaxRetailShopId();

            Console.WriteLine("Start indexing {0} Retail Shops", maxRetailShopId);

            int chunkSize = 1000;
            var tasks = new List<Task>();
            var startIndex = 0;
            while (startIndex < maxRetailShopId)
            {
                var shopList = GetChunkRetailerShop(startIndex, chunkSize);

                var docList = new List<RetailShopDoc>();
                foreach (var shop in shopList)
                {
                    var doc = new RetailShopDoc()
                    {
                        RetailShopId = shop.RetailShopId,
                        RetailerId = shop.RetailerId,
                        Name = shop.Name,
                        DisplayName = shop.DisplayName,
                        CityId = shop.CityId,
                        Address = shop.Address,
                        Latitude = shop.Latitude.Value,
                        Longitude = shop.Latitude.Value,
                        OpenTime = shop.OpenTime,
                        CloseDay = shop.CloseDay,
                        Phone = shop.Phone,
                        Enabled = shop.Enabled,
                        TaxFree = shop.TaxFree,
                        Unionpay = shop.Unionpay
                    };
                    docList.Add(doc);                  
                }

                //tasks.Add(
                try
                {
                    //Task.Run(
                    //() => 
                    Client.IndexDocuments(docList);
                        //);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                    //);

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
                Console.WriteLine("{0} retail shop indexed", startIndex);                
            }
        }

        public int GetMaxRetailShopId()
        {
            return _repositoryManager.RetailerShopRepository.Table.Max(m => m.RetailShopId);
        }

        public List<RetailerShop> GetChunkRetailerShop(int index, int size)
        {
            return _repositoryManager.RetailerShopRepository.Table.Where(m => m.RetailShopId >= index && m.RetailShopId < index + size).ToList();
        }
    }
}
