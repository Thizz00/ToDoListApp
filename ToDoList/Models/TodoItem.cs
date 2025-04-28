// Models/TodoItem.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class TodoItem
    {
        public TodoItem()
        {
            CreatedAt = DateTime.Now;
        }
        
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Tytuł zadania jest wymagany")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        
        [Display(Name = "Opis")]
        public string Description { get; set; }
        
        [Display(Name = "Zakończone")]
        public bool IsCompleted { get; set; }
        
        [Display(Name = "Data utworzenia")]
        public DateTime CreatedAt { get; set; }
        
        [Display(Name = "Data zakończenia")]
        public DateTime? CompletedAt { get; set; }
        
        [Display(Name = "Termin")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        
        [Display(Name = "Priorytet")]
        public int? Priority { get; set; }
    }
}
