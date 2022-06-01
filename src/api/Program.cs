using infrastructure;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMongoDBHelper(cfg => {
        cfg.ConnectionString = Environment.GetEnvironmentVariable("DBHOST");
        cfg.DatabaseName =  Environment.GetEnvironmentVariable("DBNAME");
});
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();


var app = builder.Build();


app.MapControllers();

app.Run();