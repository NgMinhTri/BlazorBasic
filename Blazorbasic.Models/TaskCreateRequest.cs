using Blazorbasic.Models.Enums;
using System;

namespace Blazorbasic.Models
{
    public class TaskCreateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Priority? Priority { get; set; }
    }
}
