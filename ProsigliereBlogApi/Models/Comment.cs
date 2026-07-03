namespace ProsigliereBlogApi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Author { get; set; } 

        public string Content { get; set; } 

        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}
