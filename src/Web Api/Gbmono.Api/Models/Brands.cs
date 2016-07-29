using System;
using System.Collections;
using System.Collections.Generic;

namespace Gbmono.Api.Models
{
    public class BrandSimpleModel
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

        public string FirstAlphabet { get; set; }
    }

    public class BrandAlphabetGroup
    {
        public string Alphabet { get; set; }

        public IEnumerable<BrandSimpleModel> Brands { get; set; }
    }
}