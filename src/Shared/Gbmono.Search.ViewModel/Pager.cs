using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.ViewModel
{
    public class Pager
    {
        private int _pageSize;

        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    //default PageSize:10
                    _pageSize = 10;
                }
                return _pageSize;
            }
            set
            {
                _pageSize = value;

            }
        }


        private int _pageNumber;

        public int PageNumber
        {
            get
            {
                if (_pageNumber <= 0)
                {
                    //Default PageNum:1
                    _pageNumber = 1;
                }
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }

        }

        public long Total { get; set; }
    }
}
