using System;

namespace Gbmono.EF.Models
{
    public class State
    {
        public int StateId { set; get; }

        public int CountryId { get; set; }

        public string Name{ set; get; }

        public string DisplayName { get; set; }
    }
}
