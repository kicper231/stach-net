using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class WebApplicationExtensions
{
    public static WebApplication CreateDatabase<TDbContext>(this WebApplication app)
        where TDbContext : DbContext

    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.EnsureCreated();

        return app;
    }
}
