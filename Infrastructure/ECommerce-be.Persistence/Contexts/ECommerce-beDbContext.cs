using ECommerce_be.Domain.Entities;
using ECommerce_be.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_be.Persistence.Contexts
{
    public class ECommerce_beDbContext : DbContext
    {
        public ECommerce_beDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker --> Entity'ler üzerinde yapılan değişikliklerin yada yeni eklenen verinin yakalanmasını sağlayan property'dir.
            // Update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar.

            foreach (var data in ChangeTracker.Entries<BaseEntity>())
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedTime = DateTime.UtcNow;
                        data.Entity.Id = Guid.NewGuid();
                        data.Entity.IsEnabled = true;
                        data.Entity.IsDeleted = false;
                        break;

                    case EntityState.Deleted:
                        data.Entity.DeletedTime = DateTime.UtcNow;
                        data.Entity.IsDeleted = true;
                        break;

                    case EntityState.Modified:
                        data.Entity.UpdatedTime = DateTime.UtcNow;
                        break;

                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
