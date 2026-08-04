using Data.BdSchemas;
using Data.Mappings.Base;
using Domain.Model.Stage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Stage;

public class StageMap : BaseMap<StageModel>
{
    public override void Configure(EntityTypeBuilder<StageModel> builder)
    {
        base.Configure(builder);

        builder.ToTable(DatabaseTables.Stage);

        builder.HasOne(x => x.Save)
            .WithMany()
            .HasForeignKey(x => x.SaveId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
