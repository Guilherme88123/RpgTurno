using Data.Repositories;
using Domain.Interface.Repositories.Unit;
using Domain.Model.Unit;

namespace Service.Repositories.Unit;

public class UnitService : EfRepository<UnitModel>, IUnitService
{
    public UnitService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
