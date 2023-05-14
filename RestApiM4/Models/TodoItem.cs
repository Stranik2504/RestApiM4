﻿using System.ComponentModel.DataAnnotations;

namespace RestApiM4.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
