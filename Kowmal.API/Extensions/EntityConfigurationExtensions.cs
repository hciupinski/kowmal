using Kowmal.API.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kowmal.API.Extensions;

public static class EntityConfigurationExtensions
{
    public static void AddPrimaryId<T>(this EntityTypeBuilder<T> entity) where T : Entity
    {
        entity.Property(e => e.SId)
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        entity.HasKey(x => x.SId);
    }
}