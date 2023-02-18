using ECommerce_be.Application.Repositories;
using ECommerce_be.Domain.Entities.Common;
using ECommerce_be.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce_be.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerce_beDbContext _context;

        public ReadRepository(ECommerce_beDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        // AsNoTracking --> Model üzerinde bir değişiklik yapılıp veritabanına kayıt etme işlemi yapılmayacaksa kullanılır.

        public IQueryable<T> GetAll(bool tracking = true)
            => tracking ? Table : Table.AsNoTracking();

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
            => tracking ? Table.Where(method) : Table.Where(method).AsNoTracking();

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
            => tracking ? await Table.FindAsync(id) : await Table.AsQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
            => tracking ? await Table.FirstOrDefaultAsync(method) : await Table.AsQueryable().AsNoTracking().FirstOrDefaultAsync(method);
    }
}
