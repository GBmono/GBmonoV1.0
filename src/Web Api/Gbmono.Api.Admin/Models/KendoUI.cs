using System;

namespace Gbmono.Api.Admin.Models
{
    // kendo ui category treeview item model
    public class KendoTreeViewItem
    {
        public int Id { get; set; } // key 

        public string Name { get; set; } // name

        public bool Expanded { get; set; } // is expaneded

        public bool HasChildren { get; set; } // if any child node exists

        public string LinksTo { get; set; } // url link
    }

    public class KendoChartBase
    {
        public string Category { get; set; }
    }

    // kendo ui product count buy category model
    public class KendoBarChartItem: KendoChartBase
    {
        public int ProductCount { get; set; }
    }

}