namespace ClashOfMusic.Api.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PlayListId { get; set; }
        public UserViewModel User { get; set; }
        public string? Created { get; set; }
    }
}
