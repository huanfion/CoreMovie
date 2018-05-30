using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBenk.APi.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public IList<MaterialDto> Materials { get; set; }
    }
}
