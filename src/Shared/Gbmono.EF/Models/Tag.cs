﻿using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public short TagTypeId { get; set; }
    }
}
