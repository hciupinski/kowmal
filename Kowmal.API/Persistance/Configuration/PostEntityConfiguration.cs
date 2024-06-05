using Kowmal.API.Extensions;
using Kowmal.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kowmal.API.Persistance.Configuration;

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