using Minimal_API_day_1.Domain;

namespace Minimal_API_day_1
{
    public interface ILearningService
    {
        Task<IEnumerable<LearningTask>> GetAll();
        Task<LearningTask>? GetById(int id);
        Task<LearningTask> Add(TaskDto dto);
        Task<bool> Update(int id, TaskDto dto);
        Task<bool> Delete(int id);
    }
}
