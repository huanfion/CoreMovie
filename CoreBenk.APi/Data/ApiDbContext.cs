using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using CoreBenk.APi.Entities;
using CoreBenk.APi.EntityMapping;
using Microsoft.EntityFrameworkCore;

namespace CoreBenk.APi.Data
{
    public class ApiDbContext : DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //EnsureCreated()的作用是，如果有数据库存在，那么什么也不会发生。但是如果没有，那么就会创建一个数据库。
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //我们就会发现一个严重的问题。如果项目里面有很多entity，那么所有的fluent api配置都需要写在OnModelCreating这个方法里，那太多了
            #region ======== 过时方式========
            //modelBuilder.Entity<Product>().HasKey(x => x.Id);
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            //modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(8,2)");
            #endregion
            //但是项目中如果有很多entities的话也需要写很多行代码，更好的做法是写一个方法，可以加载所有实现了IEntityTypeConfiguration<T>的实现类。
            #region =====次优方式=====

            //modelBuilder.ApplyConfiguration(new ProductMapping());

            #endregion

            #region 最优方式
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.GetInterfaces().Any(interf=>interf.IsGenericType&&interf.GetGenericTypeDefinition()== typeof(IEntityTypeConfiguration<>)));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }


            #endregion
        }
    }
}
