﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.Entity
{
    public class Location : Audit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

    }
}
