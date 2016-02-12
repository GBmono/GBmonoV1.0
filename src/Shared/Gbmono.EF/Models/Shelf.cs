using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.Models
{
    //货架表
    public class Shelf
    {
        public int ShelfId { set; get; }

        public string ImageUrl { set; get; }    //货架图片

        public int ShelfDetailId { set; get; }  //细节ID，代表具体 哪一年  哪一类型  哪一实例

        public bool Active { set; get; }  //同一个细节ID，只能1个被active

    }
}
