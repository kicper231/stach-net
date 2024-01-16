using Api;
using Api.Authorization;
using Api.Service;
using Domain;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var OurUrl = $"{builder.Configuration["CourierApi:UrlLocal"]}"; //Url lub Urllocal
var SzymonUrl = $"{builder.Configuration["IdentityManager:Url"]}";
var TokenSzymonUrl = $"{builder.Configuration["IdentityManager:TokenEndpointSzymon"]}";

builder.Services.AddHttpClient("OurClient", client => { client.BaseAddress = new Uri($"{OurUrl}"); });

builder.Services.AddHttpClient("SzymonClient", client => { client.BaseAddress = new Uri($"{SzymonUrl}"); });

builder.Services.AddHttpClient("SzymonToken", client => { client.BaseAddress = new Uri($"{TokenSzymonUrl}"); });
//System.Console.WriteLine(builder.Configuration["CourierApi:Url"]);

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

builder.Services.AddScoped<ICourierCompanyRepository, DbCourierCompanyRepository>();
builder.Services.AddScoped<IOfferService,OfferService>();
builder.Services.AddScoped<IOfferRepository, DbOfferRepository>();
builder.Services.AddScoped<IInquiryServiceFactory, InquiryServiceFactory>();

builder.Services.Configure<IdentityManagerSettings>(builder.Configuration.GetSection("IdentityManager"));


builder.Services.AddScoped<IDeliveryRequestService, DeliveryRequestService>();
builder.Services.AddScoped<IEmailService,EmailService>();



// Konfiguracja factory do tworzenia DbContext, konkretnie ShopperContext, u�ywaj�c SQLite jako bazy danych
builder.Services.AddDbContextFactory<ShopperContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HostowanaBaza"),
        x => x.MigrationsAssembly("Api")
    )
);


// servis bazy danych
builder.Services.AddHostedService<DbCreationalService>();

// autetykacja
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
    });

//autoryzacja pozniej
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("client:permission",
        policy => policy.Requirements.Add(new HasScopeRequirement("client:permission", domain)));
    options.AddPolicy("courier:permission",
        policy => policy.Requirements.Add(new HasScopeRequirement("courier:permission", domain)));
    options.AddPolicy("serviceworker:permission",
        policy => policy.Requirements.Add(new HasScopeRequirement("serviceworker:permission", domain)));
});




builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


var app = builder.Build(); // Buduje aplikacj� webow�


if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI(); // ui nawet gdy nie developmnet


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Mapuje trasy do akcji kontroler�w


app.Run(); // Uruchamia aplikacj�