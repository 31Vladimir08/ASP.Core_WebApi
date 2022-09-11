using MongoDB.Driver;
using ProductApi.DependencyInjection;
using ProductApi.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProductMongoDbSettings>(builder.Configuration.GetSection("ProductMongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(x =>
    new MongoClient(builder.Configuration.GetValue<string>("ProductMongoDbSettings:ConnectionString")));
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
