using ExchangeRatesV2.Application.Repositories;
using ExchangeRatesV2.Core.Fetchers.Fixer;
using ExchangeRatesV2.Core.Interfaces;
using ExchangeRatesV2.Data;
using ExchangeRatesV2.Data.Repositories;
using ExchangeRatesV2.Web.Health;
using ExchangeRatesV2.Web.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("Database")
                                  .AddCheck<FetcherHealthCheck>("Fixer");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>
    (
        d => d.UseNpgsql(builder.Configuration.GetConnectionString("ExchangeRatesDb"))
    );
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddSingleton<IFetcher>(opt => new FixerFetcher(builder.Configuration.GetValue<string>("ApiURLS:FixerUrls:APIKey"), builder.Configuration.GetValue<string>("ApiURLS:FixerUrls:URL")));
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/_health",new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
