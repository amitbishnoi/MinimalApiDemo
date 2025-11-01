using System.ComponentModel.DataAnnotations;

namespace Minimal_API_day_1.Domain
{
    public class LearningTask
    {
        [Key]
        public int TaskId { get; set; }
        public required string TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
    }
}
