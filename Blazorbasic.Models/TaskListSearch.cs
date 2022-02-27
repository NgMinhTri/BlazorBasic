using Blazorbasic.Models.Enums;
using System;

namespace Blazorbasic.Models
{
    public  class TaskListSearch
    {
        public string Name { get; set; }

        public Guid? AssignerId { get; set; }

        public Priority? Priority { get; set; }
    }
}
