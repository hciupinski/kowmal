using Kowmal.App.Extensions;
using Kowmal.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kowmal.App.Persistance.Configuration;

public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.AddPrimaryId();
    }
}