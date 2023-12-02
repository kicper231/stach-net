using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class WebApplicationExtensions
{
    // Definicja metody rozszerzenia CreateDatabase dla klasy WebApplication
    public static WebApplication CreateDatabase<TDbContext>(this WebApplication app)
        where TDbContext : DbContext // Ograniczenie generyczne, TDbContext musi być typem pochodnym od DbContext
    {
        // Tworzenie zakresu (scope), który pozwala na uzyskanie usług z kontenera DI
        using var scope = app.Services.CreateScope();

        // Pobieranie instancji TDbContext z kontenera usług
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        // Upewnienie się, że baza danych dla kontekstu DbContext istnieje. Jeśli nie, zostanie utworzona.
        context.Database.EnsureCreated();

        // Zwracanie aplikacji, umożliwia to łańcuchowe wywoływanie metod (chaining)
        return app;
    }
}

