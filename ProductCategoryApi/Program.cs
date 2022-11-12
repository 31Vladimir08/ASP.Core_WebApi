using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using ProductCategoryApi;
using ProductCategoryApi.DependencyInjection;
using ProductCategoryApi.Messaging;
using ProductCategoryApi.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapProfiler));

builder.Services.Configure<CategoryMongoDbSettings>(builder.Configuration.GetSection("CategoryMongoDbSettings"));
builder.Services.Configure<AzureServiceBusSettings>(builder.Configuration.GetSection("AzureServiceBus"));
builder.Services.AddSingleton<IMongoClient>(x => 
    new MongoClient(builder.Configuration.GetValue<string>("CategoryMongoDbSettings:ConnectionString")));
builder.Services.SetRepositoriesDJ();
builder.Services.SetServicesDJ();
builder.Services.SetAzureServiceBusDJ(builder.Configuration.GetValue<string>("AzureServiceBus:ServiceBusConnectionString"));
builder.Services.AddHostedService<AzureServiceBusConsumerHostedService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
