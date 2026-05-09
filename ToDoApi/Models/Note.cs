namespace ToDoApi.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
    }
}