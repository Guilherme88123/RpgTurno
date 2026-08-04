using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories;

public class EfBase : IDisposable
{
    protected readonly DbContext DbContext;
    protected readonly IServiceProvider _serviceProvider;

    public EfBase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        DbContext = serviceProvider.GetRequiredService<DbContext>();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}
