namespace MyApi.DTOs.Posts
{
    public class EditPostDto
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
