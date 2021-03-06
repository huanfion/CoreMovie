﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBenk.APi.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Materials = new List<MaterialDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IList<MaterialDto> Materials { get; set; }
        public int MaterialCount => Materials.Count;
    }
}
