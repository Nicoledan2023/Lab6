namespace BlogApi.Models
{
    public class BlogModel
    {
        public uint BlogModelId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PostedTime { get; set; } = DateTime.Now;
    }
}