using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}