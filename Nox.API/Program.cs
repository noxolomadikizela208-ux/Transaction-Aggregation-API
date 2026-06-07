using Microsoft.OpenApi.Models;
using Nox.API.Application.Interfaces;
using Nox.API.Application.Services;
using Nox.API.Infrastructure.Sources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITransactionAggregateService, TransactionAggregateService>();
builder.Services.AddScoped<ITransactionSource, BankATransactionSource>();
builder.Services.AddScoped<ITransactionSource, BankBTransactionSource>();
builder.Services.AddScoped<TransactionCategorizationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nox API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
