using System;

namespace Gbmono.EF.Models
{
    public class City
    {
        public int CityId { get; set; }

        public int StateId { set; get; }
        public State State { set; get; }


        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}
