using Minimal_API_day_1.Domain;

namespace Minimal_API_day_1.Application
{
    public interface ILearningTaskRepository : IRepository<LearningTask>
    {
        Task<IEnumerable<LearningTask>> GetByNameAsync(string title);
    }
}