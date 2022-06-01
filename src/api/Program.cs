using infrastructure;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

Config config = new Config(){
    ConnectionString = Environment.GetEnvironmentVariable("DBHOST"),
    DatabaseName = Environment.GetEnvironmentVariable("DBNAME"),
};

builder.Services.AddSingleton<IConfig>(config);
builder.Services.AddScoped<IMongoDBHelper, MongoDBHelper>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();


var app = builder.Build();


app.MapControllers();

app.Run();