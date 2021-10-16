using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Enums;

namespace TodoListApi.Entities
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        //Lesson 10
        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; }

    }
}
