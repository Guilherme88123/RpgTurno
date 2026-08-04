using Data.Mappings.Save;
using Data.Mappings.Settings;
using Data.Mappings.Stage;
using Data.Mappings.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new SettingsMap());
        modelBuilder.ApplyConfiguration(new SaveMap());
        modelBuilder.ApplyConfiguration(new UnitMap());
        modelBuilder.ApplyConfiguration(new StageMap());
    }
}
