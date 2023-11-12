using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class DbCreationalService : IHostedService
{
    private readonly IDbContextFactory<ShopperContext> _contextFactory;

    public DbCreationalService(IDbContextFactory<ShopperContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    { 
        var context = await _contextFactory.CreateDbContextAsync();

        await context.Database.EnsureCreatedAsync();
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}