using Data.BdSchemas;
using Data.Mappings.Base;
using Domain.Model.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Unit;

public class UnitMap : BaseMap<UnitModel>
{
    public override void Configure(EntityTypeBuilder<UnitModel> builder)
    {
        base.Configure(builder);

        builder.ToTable(DatabaseTables.Unit);
    }
}
