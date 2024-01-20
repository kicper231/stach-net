
using Api;
using Api.Service;
using Api.Infrastructure;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Api.Authorization;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

    // Definicja bezpieczeñstwa JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Wymaganie bezpieczeñstwa
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services
  .AddAuthorization(options =>
  {
      options.AddPolicy(
        "read:messages",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:messages", domain)
        )
      );
  });

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
//builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<IDeliveryRequest, Inquiries>();
builder.Services.AddDbContextFactory<ShopperContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HostowanaBaza"),
        x => x.MigrationsAssembly("Api")
    )
);
builder.Services.AddHostedService<DbCreationalService>();

var app = builder.Build();
// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
