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
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
