using Domain.Interface.Repositories.Base;
using Domain.Model.Settings;

namespace Domain.Interface.Repositories.Settings;

public interface ISettingsService : IRepository<SettingsModel>
{
    Task<bool> AnyAsync();
    Task<SettingsModel> GetAsync();
}
