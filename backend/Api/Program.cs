using Api;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(); // Dodaje us�ugi obs�ugi kontroler�w MVC do aplikacji
builder.Services.AddEndpointsApiExplorer(); // Umo�liwia eksploracj� endpoint�w API, przydatne dla Swagger
builder.Services.AddSwaggerGen();
// Dodaje generator Swaggera, kt�ry dostarcza UI do testowania API i dokumentacji
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Dodanie us�ugi scoped dla ICustomerRepository, kt�ra b�dzie u�ywa� DbCustomerRepository do swojej implementacji
builder.Services.AddScoped<ICustomerRepository, DbCustomerRepository>();

// Konfiguracja factory do tworzenia DbContext, konkretnie ShopperContext, u�ywaj�c SQLite jako bazy danych
builder.Services.AddDbContextFactory<ShopperContext>(options =>
  options.UseSqlite("Data Source=shopper.db"));

// builder.Services.AddDbContextFactory<ShopperContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("HostowanaBaza")));

//builder.Services.AddDbContextFactory<ShopperContext>(options =>
//    options.UseSqlServer("data source=DESKTOP-IIG9H2J;initial catalog=stachnetest;user id=sa;password=monia231; TrustServerCertificate=True "));


//builder.Services.AddDbContextFactory<ShopperContext>(options =>
//    options.UseSqlServer("data source=DESKTOP-IIG9H2J;initial catalog=stachnetest;user id=sa;password=monia231; TrustServerCertificate=True "));

// Odkomentuj, aby doda� us�ug� hostowan�, kt�ra uruchomi DbCreationalService przy starcie
builder.Services.AddHostedService<DbCreationalService>();

var app = builder.Build(); // Buduje aplikacj� webow�

// Konfiguruje pipeline ��da� HTTP.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Dodaje middleware do przekierowywania ��da� HTTP na HTTPS
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization(); // Dodaje middleware do autoryzacji

app.MapControllers(); // Mapuje trasy do akcji kontroler�w

// Ta niestandardowa metoda rozszerzenia (nie jest cz�ci� ASP.NET Core) prawdopodobnie s�u�y do upewnienia si�, �e baza danych jest tworzona przy starcie aplikacji.
//app.CreateDatabase<ShopperContext>();

app.Run(); // Uruchamia aplikacj�
