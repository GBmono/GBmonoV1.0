using System;

namespace Gbmono.Api.Admin.Models
{
    // kendo ui treeview item model
    public class KendoTreeViewItem
    {
        public int Id { get; set; } // key 

        public string Name { get; set; } // name

        public bool Expanded { get; set; } // is expaneded

        public bool HasChildren { get; set; } // if any child node exists

        public string LinksTo { get; set; } // url link
    }
}