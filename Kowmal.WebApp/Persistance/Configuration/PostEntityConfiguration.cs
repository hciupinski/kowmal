using Kowmal.WebApp.Extensions;
using Kowmal.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kowmal.WebApp.Persistance.Configuration;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.AddPrimaryId();

        builder.HasMany<Photo>(p => p.Photos)
            .WithOne(x => x.Post)
            .HasForeignKey(p => p.PostId).IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}