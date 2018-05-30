using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBenk.APi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBenk.APi.EntityMapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public   void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");
        }
    }
}
