using ECommerce_be.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_be.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table { get; }
    }
}
