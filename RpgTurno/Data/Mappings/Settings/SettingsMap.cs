using Data.BdSchemas;
using Data.Mappings.Base;
using Domain.Model.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Settings;

public class SettingsMap : BaseMap<SettingsModel>
{
    public override void Configure(EntityTypeBuilder<SettingsModel> builder)
    {
        base.Configure(builder);

        builder.ToTable(DatabaseTables.Settings);
    }
}
