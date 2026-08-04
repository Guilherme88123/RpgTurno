using Domain.Interface.Repositories;
using Domain.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class EfRepository<TModel> : EfBase, IRepository<TModel> where TModel : BaseModel
{
    protected readonly DbSet<TModel> DbSet;

    public EfRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        DbSet = DbContext.Set<TModel>();
    }

    public virtual bool IsValid(TModel model)
    {
        return true;
    }

    public virtual Task<TModel> CreateAsync(TModel model)
    {
        return CreateAsync(model, saveChanges: true);
    }

    public virtual async Task<TModel> CreateAsync(TModel model, bool saveChanges)
    {
        if (!IsValid(model))
            return model;

        model.Id = GetNewId();

        DbSet.Add(model);

        if (saveChanges)
            await SaveChangesAsync();

        return model;
    }

    protected virtual Guid GetNewId()
    {
        return Guid.NewGuid();
    }

    protected virtual IQueryable<TModel> Query()
    {
        return DbSet.AsNoTracking();
    }

    protected virtual IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate)
    {
        return Query().Where(predicate);
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        return await Query().ToListAsync();
    }

    public async Task<TModel> GetAsync(Guid id)
    {
        return await Query().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AnyAsync(Guid id)
    {
        return await Query().AnyAsync(x => x.Id == id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        await DeleteAsync(model);
    }

    public async Task DeleteAsync(Guid id, bool saveChanges = true)
    {
        var model = await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        await DeleteAsync(model, saveChanges);
    }

    protected virtual Task DeleteAsync(TModel model)
    {
        return DeleteAsync(model, saveChanges: true);
    }

    protected virtual async Task DeleteAsync(TModel model, bool saveChanges = true)
    {
        DbSet.Remove(model);

        if (saveChanges)
        {
            await SaveChangesAsync();
            DbContext.Entry(model).State = EntityState.Detached;
        }
    }

    public int SaveChanges()
    {
        var result = DbContext.SaveChanges();
        DetachEntities();

        return result;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        var result = await DbContext.SaveChangesAsync();
        DetachEntities();

        return result;
    }

    public Task UpdateAsync(TModel model)
    {
        return UpdateAsync(model, saveChanges: true);
    }

    public async Task UpdateAsync(TModel model, bool saveChanges = true, params Expression<Func<TModel, object>>[] updatedProperties)
    {
        if (!IsValid(model))
            return;

        if (updatedProperties != null && updatedProperties.Length > 0)
        {
            DbContext.Entry(model).State = EntityState.Unchanged;
            foreach (var property in updatedProperties)
            {
                DbContext.Entry(model).Property(property).IsModified = true;
            }
        }
        else
        {
            DbContext.Entry(model).State = EntityState.Modified;
        }

        await BeforeUpdateAsync(model);

        if (saveChanges)
            await SaveChangesAsync();
    }

    protected virtual Task BeforeUpdateAsync(TModel model)
    {
        return Task.CompletedTask;
    }

    private void DetachEntities()
    {
        foreach (var item in DbContext.ChangeTracker.Entries<TModel>())
        {
            item.State = EntityState.Detached;
        }
    }
}
