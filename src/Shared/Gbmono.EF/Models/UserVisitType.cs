using System;


namespace Gbmono.EF.Models
{
    public enum UserVisitType
    {
        ProductScan = 1, // scan product barcodes

        ProductView = 2, // view products

        ArticleView = 3 // view articles
    }
}
