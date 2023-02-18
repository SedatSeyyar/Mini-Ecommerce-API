using ECommerce_be.Domain.Entities.Common;
using System.Linq.Expressions;

// IEnumerable --> In Memory'de çalışır. Tüm veriyi çeker kalan işleme ondan sonra devam eder.
// IQueryable --> Giden query'i düzenler ve belirlenen şartlara uygun data çeker.
namespace ECommerce_be.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
    }
}
