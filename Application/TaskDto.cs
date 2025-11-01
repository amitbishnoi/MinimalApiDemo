using System.Xml.Linq;

namespace Minimal_API_day_1
{
    public class TaskDto
    {
        public required string TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public bool IsValid(out string error)
        {
            if (string.IsNullOrWhiteSpace(TaskTitle))
            {
                error = "Course name is required.";
                return false;
            }
            error = string.Empty;
            return true;
        }
    }
}
