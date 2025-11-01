namespace Minimal_API_day_1.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Minimal_API_day_1.Application;
    using Minimal_API_day_1.Data;

    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        public EfRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _db.FindAsync<T>(id);

        public async Task<T> AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task<bool> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            return Task.FromResult(true); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _db.Set<T>().Remove(entity);
            return true;
        }
        public async Task<bool> SaveChangesAsync()
        {
            var saved = await _db.SaveChangesAsync();
            return saved > 0;
        }
    }

}
