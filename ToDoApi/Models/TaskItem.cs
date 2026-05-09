using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace ToDoApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва завдання не може бути порожньою")]
        public string? Title { get; set; }
        public DateTime Deadline { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<SubTask> SubTasks { get; set; } = new();
        public List<Note> Notes { get; set; } = new();
        public List<Attachment> Attachments { get; set; } = new();
    }
}