using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;

namespace Gbmono.Crawler.Processor
{
    public class ProductTagRandomAssign
    {
        public void RandomMapping()
        {
            var _repoManager = new RepositoryManager();

            var tags = _repoManager.TagRepository.Table.ToList();

            var products = _repoManager.ProductRepository.Table.ToList();


            foreach (var product in products.AsParallel())
            {
                if (!_repoManager.ProductTagRepository.Table.Any(m => m.ProductId == product.ProductId))
                {
                    var productTags = tags.OrderBy(m => Guid.NewGuid()).Take(5).ToList();
                    foreach (var productTag in productTags)
                    {
                        _repoManager.ProductTagRepository.Create(new ProductTag() { ProductId = product.ProductId, TagId = productTag.TagId });
                    }
                    _repoManager.ProductTagRepository.Save();
                    Console.Write("+");
                }
               
            }
        }


    }
}
