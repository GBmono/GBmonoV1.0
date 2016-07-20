using System.Linq;

using Gbmono.EF.Models;
using Gbmono.Api.Models;

namespace Gbmono.Api.Extensions
{
    public static class ArticleExtension
    {
        private const string _noImgUrl = "";

        public static ArticleSimpleModel ToSimpleToModel(this Article article)
        {
            return new ArticleSimpleModel
            {
                ArticleId = article.ArticleId,
                ArticleTypeId = article.ArticleTypeId,
                Title = article.Title,
                CoverThumbnailUrl = article.Images.FirstOrDefault(m => m.IsCoverImage) != null
                                    ? article.Images.FirstOrDefault(m => m.IsCoverImage).ThumbnailUrl
                                    : _noImgUrl,
                ModifiedBy = article.ModifiedBy,
                ModifiedDate = article.ModifiedDate
            };
        }
    }
}