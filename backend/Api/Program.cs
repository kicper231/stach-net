using Api;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Api.Service;
using Microsoft.OpenApi.Models;
using Api.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

//Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

// Definicja schematu bezpieczeństwa dla tokena Bearer
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
});

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });



});



//cors polityka 
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});
//dodanie scopow injections
builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<IPackageRepository, DbPackageRepository>();
builder.Services.AddScoped<IAddressRepository, DbAddressRepository>();
builder.Services.AddScoped<IDeliveryRequestRepository, DbRequestRepository>();
builder.Services.AddScoped<IUserService, UserService>();
// popraw 
builder.Services.AddHttpClient<IOfferService, OfferService>();



builder.Services.AddScoped<IDeliveryRequestService, DeliveryRequestService>();


// Konfiguracja factory do tworzenia DbContext, konkretnie ShopperContext, u�ywaj�c SQLite jako bazy danych


builder.Services.AddDbContextFactory<ShopperContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HostowanaBaza"),
        x => x.MigrationsAssembly("Api")
    )
);


//builder.Services.AddDbContextFactory<ShopperContext>(options =>
//    options.UseSqlServer("data source=DESKTOP-IIG9H2J;initial catalog=stachnetest;user id=sa;password=monia231; TrustServerCertificate=True "));


//builder.Services.AddDbContextFactory<ShopperContext>(options =>
//    options.UseSqlServer("data source=DESKTOP-IIG9H2J;initial catalog=stachnetest;user id=sa;password=monia231; TrustServerCertificate=True "));


builder.Services.AddHostedService<DbCreationalService>();


var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];


});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
});


builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


var app = builder.Build(); // Buduje aplikacj� webow�









if (app.Environment.IsDevelopment())
{
   
}
app.UseSwagger();
app.UseSwaggerUI();  // ui nawet gdy nie developmnet


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Mapuje trasy do akcji kontroler�w



app.Run(); // Uruchamia aplikacj�
