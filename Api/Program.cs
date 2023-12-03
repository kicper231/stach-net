using Api;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(); // Dodaje us³ugi obs³ugi kontrolerów MVC do aplikacji
builder.Services.AddEndpointsApiExplorer(); // Umo¿liwia eksploracjê endpointów API, przydatne dla Swagger
builder.Services.AddSwaggerGen(); 
// Dodaje generator Swaggera, który dostarcza UI do testowania API i dokumentacji
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Dodanie us³ugi scoped dla ICustomerRepository, która bêdzie u¿ywaæ DbCustomerRepository do swojej implementacji
builder.Services.AddScoped<ICustomerRepository, DbCustomerRepository>();

// Konfiguracja factory do tworzenia DbContext, konkretnie ShopperContext, u¿ywaj¹c SQLite jako bazy danych
builder.Services.AddDbContextFactory<ShopperContext>(options =>
   options.UseSqlite("Data Source=shopper.db"));

// Odkomentuj, aby dodaæ us³ugê hostowan¹, która uruchomi DbCreationalService przy starcie
builder.Services.AddHostedService<DbCreationalService>();

var app = builder.Build(); // Buduje aplikacjê webow¹

// Konfiguruje pipeline ¿¹dañ HTTP.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Dodaje middleware do przekierowywania ¿¹dañ HTTP na HTTPS
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization(); // Dodaje middleware do autoryzacji

app.MapControllers(); // Mapuje trasy do akcji kontrolerów

// Ta niestandardowa metoda rozszerzenia (nie jest czêœci¹ ASP.NET Core) prawdopodobnie s³u¿y do upewnienia siê, ¿e baza danych jest tworzona przy starcie aplikacji.
//app.CreateDatabase<ShopperContext>();

app.Run(); // Uruchamia aplikacjê
