using ECommerce_be.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerce_be.Persistence
{
    // PowerShell üzerinden talimat verilmesi durumunda çalışacaktır.
    // Package Manager Console ile çalışılacaksa bu kısma gerek kalmayacaktır.
    public class DesignTimeDbComtextFactory : IDesignTimeDbContextFactory<ECommerce_beDbContext>
    {
        public ECommerce_beDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ECommerce_beDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
