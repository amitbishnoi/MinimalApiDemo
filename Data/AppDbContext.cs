namespace Minimal_API_day_1.Data
{
    using Microsoft.EntityFrameworkCore;
    using Minimal_API_day_1.Domain;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LearningTask> LearningTask => Set<LearningTask>();
    }
}
