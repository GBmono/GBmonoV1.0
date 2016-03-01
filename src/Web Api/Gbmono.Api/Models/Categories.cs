using System;
using System.Collections.Generic;

namespace Gbmono.Api.Models
{
    /// <summary>
    /// category menu binding model
    /// it has one expaned category with sub categories and collapsed categories
    /// </summary>
    public class CategoryMenu
    {
        public ExpandedCategoryMenuItem ExpandedItem { get; set; }

        public IEnumerable<CategoryMenuItem> CollapsedItems { get; set; }
    }

    public class CategoryMenuItem
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }

    public class ExpandedCategoryMenuItem
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public IEnumerable<CategoryMenuItem> SubItems { get; set; }
    }
}