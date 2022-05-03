using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using UsersTest.Interfaces;

public class DataJob<TModel> : IJob
where TModel : class, IDbDocument
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DataJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var cacheOperation = scope.ServiceProvider.GetService<IСacheDbOperation<TModel>>();
            var dbOperation = scope.ServiceProvider.GetService<IDbOperation<TModel>>();
            var updates = cacheOperation!.GetUpdatesAndClear();
            try
            {
                await dbOperation!.UpdateMany(updates);
            }
            catch (Exception e)
            {
                cacheOperation.AddUpdates(updates);
            }
        }
    }
}