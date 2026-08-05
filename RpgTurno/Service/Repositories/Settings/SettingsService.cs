using Data.Repositories;
using Domain.Interface.Repositories.Settings;
using Domain.Model.Settings;
using Microsoft.EntityFrameworkCore;

namespace Service.Repositories.Settings;

public class SettingsService : EfRepository<SettingsModel>, ISettingsService
{
    public SettingsService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<bool> AnyAsync()
    {
        return await Query().AnyAsync();
    }

    public async Task<SettingsModel> GetAsync()
    {
        return await Query().FirstOrDefaultAsync();
    }
}
