using Domain.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Base;

public abstract class BaseMap<TModel> : IEntityTypeConfiguration<TModel> where TModel : BaseModel
{
    public void Configure(EntityTypeBuilder<TModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
