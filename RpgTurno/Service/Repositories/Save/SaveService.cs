using Data.Repositories;
using Domain.Interface.Repositories.Save;
using Domain.Model.Save;

namespace Service.Repositories.Save;

public class SaveService : EfRepository<SaveModel>, ISaveService
{
    public SaveService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
