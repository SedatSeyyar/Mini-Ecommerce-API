using ECommerce_be.Domain.Entities.Common;

namespace ECommerce_be.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> model);
        bool Remove(T model);
        bool RemoveRange(List<T> model);
        Task<bool> RemoveAsync(Guid id);
        bool Update(T model);

        Task<int> SaveAsync();
    }
}
