using Kowmal.WebApp.Extensions;
using Kowmal.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kowmal.WebApp.Persistance.Configuration;

public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.AddPrimaryId();
    }
}