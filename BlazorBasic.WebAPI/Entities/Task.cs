using BlazorBasic.WebAPI.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBasic.WebAPI.Entities
{
    public class Task
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public Guid? AssignerId { get; set; }

        [ForeignKey("AssignerId")]
        public User Assigner { get; set; }
        public DateTime CreatedDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
