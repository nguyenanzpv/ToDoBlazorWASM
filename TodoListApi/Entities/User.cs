using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TodoListApi.Entities
{
    public class User:IdentityUser<Guid>
    {
        [MaxLength(250)]
        [Required]
        public string firstName { get; set; }

        [MaxLength(250)]
        [Required]
        public string lastName { get; set; }
    }
}
