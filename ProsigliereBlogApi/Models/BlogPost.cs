using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsigliereBlogApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string Title { get; set; } 

        public string Content { get; set; } 

        public ICollection<Comment> Comments { get; set; } 
    }
}
