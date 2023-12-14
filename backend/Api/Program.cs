using Api;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Api.Service;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(); // Dodaje us�ugi obs�ugi kontroler�w MVC do aplikacji
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();


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
builder.Services.AddScoped<IUserRepository, DbRepository>();

builder.Services.AddScoped<IDeliveryRequestRepository, DbRequestRepository>();


builder.Services.AddScoped<IDeliveryRequestService, DeliveryRequestService>();


// Konfiguracja factory do tworzenia DbContext, konkretnie ShopperContext, u�ywaj�c SQLite jako bazy danych
////builder.Services.AddDbContextFactory<ShopperContext>(options =>
////  options.UseSqlite("Data Source=shopper.db"));

builder.Services.AddDbContextFactory<ShopperContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LokalnaBaza"),
        x => x.MigrationsAssembly("Api")
    )
);


//builder.Services.AddDbContextFactory<ShopperContext>(options =>
//    options.UseSqlServer("data source=DESKTOP-IIG9H2J;initial catalog=stachnetest;user id=sa;password=monia231; TrustServerCertificate=True "));


//builder.Services.AddDbContextFactory<ShopperContext>(options =>
//    options.UseSqlServer("data source=DESKTOP-IIG9H2J;initial catalog=stachnetest;user id=sa;password=monia231; TrustServerCertificate=True "));

// Odkomentuj, aby doda� us�ug� hostowan�, kt�ra uruchomi DbCreationalService przy starcie
builder.Services.AddHostedService<DbCreationalService>();


var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
});

var app = builder.Build(); // Buduje aplikacj� webow�









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
