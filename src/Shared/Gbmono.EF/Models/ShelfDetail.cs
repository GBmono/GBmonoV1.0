using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.Models.Models
{
    public class ShelfDetail
    {
        public int ShelfDetailId { set; get; }  //细节ID

        public int ShelfTypeId { set; get; }  //refer to enum "ShelfType"

        public int Year { set; get; }//年份  2016，2017

        public int InstanceId { set; get; }   //BrandId | CategoryId | （1，2，3--52）代表1到52周|（1，2，3，4） 代表季度
    }
}
