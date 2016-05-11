using System;

namespace Gbmono.Api.Admin.Models
{
    #region kendo ui treeview
    public class KendoTreeViewItem
    {
        public int Id { get; set; } // key 

        public string Name { get; set; } // name

        public bool Expanded { get; set; } // is expaneded

        public bool HasChildren { get; set; } // if any child node exists

        public string LinksTo { get; set; } // url link
    }
    #endregion

    #region kendo ui charts
    public class KendoChartBase
    {
        public string Category { get; set; }
    }

    // kendo ui product count buy category model
    public class KendoBarChartItem : KendoChartBase
    {
        public int ProductCount { get; set; }
    }
    #endregion

    #region kendo ui upload
    public class KendoUploadImg
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Size { get; set; }    
    }
    #endregion

}