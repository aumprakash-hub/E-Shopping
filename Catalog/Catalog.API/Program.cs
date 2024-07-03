using System.Reflection;
using Catalog.Application.Handlers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection serviceCollection = builder.Services;
serviceCollection.AddControllers();
serviceCollection.AddApiVersioning();
serviceCollection.AddHealthChecks().AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"], "Catalog  Mongo Db Health Check",
    HealthStatus.Degraded);
serviceCollection.AddEndpointsApiExplorer();

serviceCollection.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.SwaggerDoc("v1",         new OpenApiInfo
        {
            Title = "Catalog.API",
            Version = "v1",
            Description = "Clean architecture using Microservices for Catalog API.",
            Contact = new OpenApiContact()
            {
                Name = "Aumprakash Dehury",
                Email = "aumprakash@outlook.com",
                Url = new Uri("https://github.com/aumprakash-hub/E-Shopping/tree/EShopping-Service")
            }
        });
});

// DI registration for services
serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).GetTypeInfo().Assembly));
// serviceCollection.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
serviceCollection.AddScoped<ICatalogContext, CatalogContext>();
serviceCollection.AddScoped<IProductRepository, ProductRepository>();
serviceCollection.AddScoped<IBrandRepository, ProductRepository>();
serviceCollection.AddScoped<ITypesRepository, ProductRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();