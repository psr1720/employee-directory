﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.Entity
{
    public class Department: Audit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
