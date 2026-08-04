using Domain.Model.Base;
using System.Linq.Expressions;

namespace Domain.Interface.Repositories.Base;

public interface IRepository<TModel> where TModel : BaseModel
{
    Task<TModel> CreateAsync(TModel model);
    Task<TModel> CreateAsync(TModel model, bool saveChanges);

    Task UpdateAsync(TModel model);
    Task UpdateAsync(TModel model, bool saveChanges = true,
            params Expression<Func<TModel, object>>[] updatedProperties);

    Task DeleteAsync(Guid id);
    Task DeleteAsync(Guid id, bool saveChanges = true);

    Task<int> SaveChangesAsync();
    int SaveChanges();

    Task<bool> AnyAsync(Guid id);
    Task<TModel> GetAsync(Guid id);
    Task<IEnumerable<TModel>> GetAllAsync();
}
