
using Api;
using Api.Service;
using Api.Infrastructure;
using Domain.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();


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

app.MapControllers();

app.Run();
