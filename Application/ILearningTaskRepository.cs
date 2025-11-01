using Minimal_API_day_1.Domain;

namespace Minimal_API_day_1.Infrastructure
{
    public interface ILearningTaskRepository : IRepository<LearningTask>
    {
        Task<IEnumerable<LearningTask>> GetByNameAsync(string title);
    }
}