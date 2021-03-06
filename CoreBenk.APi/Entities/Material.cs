﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBenk.APi.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public virtual Product product { get; set; }
    }
}
