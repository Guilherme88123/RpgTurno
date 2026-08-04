using Data.Repositories;
using Domain.Interface.Repositories.Stage;
using Domain.Model.Stage;
using Microsoft.EntityFrameworkCore;

namespace Service.Repositories.Stage;

public class StageService : EfRepository<StageModel>, IStageService
{
    public StageService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<List<StageModel>> GetBySaveAsync(Guid saveId)
    {
        return await Query(x => x.SaveId == saveId).ToListAsync();
    }
}
