using infrastructure;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
Config config = new Config(){
    ConnectionString = builder.Configuration.GetSection("Config").GetValue<string>("ConnectionString"),
    DatabaseName = builder.Configuration.GetSection("Config").GetValue<string>("DatabaseName"),
};
builder.Services.AddSingleton<IConfig>(config);
builder.Services.AddScoped<IMongoDBHelper, MongoDBHelper>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


app.MapControllers();

app.Run();
