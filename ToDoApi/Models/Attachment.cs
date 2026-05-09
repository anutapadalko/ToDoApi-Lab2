namespace ToDoApi.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string? LinkUrl { get; set; }
        public string? LinkName { get; set; }
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
    }
}