using Microsoft.EntityFrameworkCore;
using Minimal_API_day_1.Application;
using Minimal_API_day_1.Data;
using Minimal_API_day_1.Domain;

namespace Minimal_API_day_1.Infrastructure
{
    public class LearningTaskRepository : EfRepository<LearningTask>, ILearningTaskRepository
    {
        public LearningTaskRepository(AppDbContext db) : base(db) { }

        public async Task<IEnumerable<LearningTask>> GetByNameAsync(string title)
                => await _db.LearningTask.Where(c => c.TaskTitle == title).ToListAsync();
        
    }
}
