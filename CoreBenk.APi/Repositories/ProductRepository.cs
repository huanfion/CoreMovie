using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBenk.APi.Data;
using CoreBenk.APi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreBenk.APi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiDbContext _context;

        public ProductRepository(ApiDbContext context)
        {
            _context = context;
        }

        public Material GetMaterialForProduct(int productId, int materialId)
        {
            return _context.Materials.FirstOrDefault(x=>x.ProductId==productId&&x.Id==materialId);
        }

        public IEnumerable<Material> GetMaterialsForProduct(int productId)
        {
            return _context.Materials.Where(x => x.ProductId == productId).ToList();
        }

        public Product GetProduct(int productId, bool includeMaterials)
        {
            if (includeMaterials)
            {
                return _context.Products.Include(x => x.Materials).FirstOrDefault(x => x.Id == productId);
            }

            return _context.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(x => x.Name).ToList();
        }
    }
}
