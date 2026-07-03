namespace ProsigliereBlogApi.DTOs.Responses
{
    public class PostResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } 

        public string Content { get; set; } 

        public List<CommentResponse> Comments { get; set; } 
    }
}
