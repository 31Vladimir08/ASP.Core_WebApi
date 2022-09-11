using MongoDB.Driver;

using ProductCategoryApi.DependencyInjection;
using ProductCategoryApi.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CategoryMongoDbSettings>(builder.Configuration.GetSection("CategoryMongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(x => 
    new MongoClient(builder.Configuration.GetValue<string>("CategoryMongoDbSettings:ConnectionString")));
builder.Services.SetRepositoriesDJ();
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
