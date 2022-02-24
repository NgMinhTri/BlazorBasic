using BlazorBasic.WebAPI.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorBasic.WebAPI.Entities
{
    public class Task
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public Guid? Assigner { get; set; }
        public DateTime CreatedDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
