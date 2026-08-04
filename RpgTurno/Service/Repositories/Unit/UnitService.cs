using Data.Repositories;
using Domain.Interface.Repositories.Unit;
using Domain.Model.Unit;
using Microsoft.EntityFrameworkCore;

namespace Service.Repositories.Unit;

public class UnitService : EfRepository<UnitModel>, IUnitService
{
    public UnitService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<List<UnitModel>> GetBySaveAsync(Guid saveId)
    {
        return await Query(x => x.SaveId == saveId).ToListAsync();
    }
}
