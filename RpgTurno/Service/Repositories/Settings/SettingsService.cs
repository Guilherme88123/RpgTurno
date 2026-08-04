using Data.Repositories;
using Domain.Interface.Repositories.Settings;
using Domain.Model.Settings;

namespace Service.Repositories.Settings;

public class SettingsService : EfRepository<SettingsModel>, ISettingsService
{
    public SettingsService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
