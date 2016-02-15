using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{ 
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryCode { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; }

        public IEnumerable<Category> SubCategories { get; set; }
    }
}
