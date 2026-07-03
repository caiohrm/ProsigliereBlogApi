using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsigliereBlogApi.Models;

namespace ProsigliereBlogApi.Data.Configurations
{
    public class BlogPostConfiguration
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("BlogPosts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.BlogPost)
                .HasForeignKey(x => x.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
