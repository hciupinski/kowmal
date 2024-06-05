using Kowmal.API.Extensions;
using Kowmal.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kowmal.API.Persistance.Configuration;

public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.AddPrimaryId();
    }
}