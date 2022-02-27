using Blazorbasic.Models.Enums;

namespace Blazorbasic.Models
{
    public class TaskUpdateRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}
