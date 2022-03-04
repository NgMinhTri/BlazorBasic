using Blazorbasic.Models.Enums;
using Blazorbasic.Models.SeedWork;
using System;

namespace Blazorbasic.Models
{
    public  class TaskListSearch : PagingParameters
    {
        public string Name { get; set; }

        public Guid? AssignerId { get; set; }

        public Priority? Priority { get; set; }
    }
}
