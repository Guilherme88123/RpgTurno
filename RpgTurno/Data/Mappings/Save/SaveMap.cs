using Data.BdSchemas;
using Data.Mappings.Base;
using Domain.Model.Save;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Save;

public class SaveMap : BaseMap<SaveModel>
{
    public override void Configure(EntityTypeBuilder<SaveModel> builder)
    {
        base.Configure(builder);

        builder.ToTable(DatabaseTables.Save);
    }
}
