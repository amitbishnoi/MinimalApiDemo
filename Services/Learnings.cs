using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Minimal_API_day_1.Application;
using Minimal_API_day_1.Data;
using Minimal_API_day_1.Domain;
using System.Threading.Tasks;

namespace Minimal_API_day_1.Services
{
    public class Learnings : ILearningService
    {
        private readonly ILearningTaskRepository _context;
        private readonly ILogger<Learnings> _logger;

        public Learnings(ILearningTaskRepository appDbContext, ILogger<Learnings> logger)
        {
            _context = appDbContext;
            _logger = logger;
        }
        public async Task<IEnumerable<LearningTask>> GetAll() => await _context.GetAllAsync();

        public async Task<LearningTask?> GetById(int id) => await _context.GetByIdAsync(id);

        public async Task<LearningTask> Add(TaskDto dto)
        {
            var learningTask = new LearningTask
            {
                TaskId = 0,
                TaskTitle = dto.TaskTitle,
                TaskDescription = dto.TaskDescription
            };
            var newLearningTask = await _context.AddAsync(learningTask);
            var isSaveChanges = await _context.SaveChangesAsync();
            _logger.LogInformation("Add new task");
            return newLearningTask;
        }

        public async Task<bool> Update(int id, TaskDto dto)
        {
            var existingTask = await _context.GetByIdAsync(id);

            if (existingTask == null)
            {
                _logger.LogWarning($"Task with ID {id} not found for update.");
                return false;
            }

            existingTask.TaskTitle = dto.TaskTitle;
            existingTask.TaskDescription = dto.TaskDescription;

            var isUpdated = await _context.UpdateAsync(existingTask);
            var isSaveChanges = await _context.SaveChangesAsync();
            _logger.LogInformation($"Task with ID {id} updated.");
            return isUpdated;
        }

        public async Task<bool> Delete(int id)
        {
            var isDeleted = await _context.DeleteAsync(id);
            var isSaveChanges = await _context.SaveChangesAsync();
            _logger.LogInformation("task deleted");
            return isDeleted;
        }
    }
}