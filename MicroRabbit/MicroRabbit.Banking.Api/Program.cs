using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Configuration.GetConnectionString("")

var config = builder.Configuration.GetConnectionString("BankingDbConnection");


//builder.services.AddTransient<BankingDbContext>();
builder.Services.AddDbContext<BankingDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbConnection"));

    }
);

builder.Services.AddMvc();

///TODO : Not sure Section 8: 39
//RegisteredServices(builder.Services);

//builder.Services.AddTransient<IEventBus, RabbitMQBus>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddSwaggerGen(cfg => {
    cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking Microservice", Version = "v1" });
    });

//Application Services

//builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountService, AccountService>(); 

//Data layer

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddControllers();  

///TODO : Not sure Section 8: 39
//void RegisteredServices(IServiceCollection services)
//{
//    DependencyContainer.RegisterServices(services);
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseSwagger();
app.MapControllers();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Banking Microservice V1");
});

app.Run();
