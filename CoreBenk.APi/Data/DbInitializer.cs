using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBenk.APi.Dtos;
using CoreBenk.APi.Entities;

namespace CoreBenk.APi.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApiDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var Products = new List<Product>
            {
                new Product
                {
                    Name = "牛奶",
                    Price = new decimal(2.5),
                    Description = "这是牛奶",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Name = "水"
                        },
                        new Material
                        {
                            Name = "奶粉"
                        }
                    }
                },
                new Product
                {
                    Name = "面包",
                    Price = new decimal(4.5),
                    Description = "这是面包",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Name = "面粉"
                        },
                        new Material
                        {
                            Name = "糖"
                        }
                    }
                },
                new Product
                {
                    Name = "啤酒",
                    Price = new decimal(7.5),
                    Description = "这是啤酒",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Name = "麦芽"
                        },
                        new Material
                        {
                            Name = "地下水"
                        }
                    }
                }
            };

            context.Products.AddRange(Products);
            context.SaveChanges();
        }
    }
}
