using BeecareDeploy.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
DeployConfig? config = 
    JsonConvert.DeserializeObject<DeployConfig>(File.ReadAllText(builder.Configuration.GetSection("DeployConfig").Value!));

    
if (config is null) throw new Exception("Deploy configuration was not found. Check the appsettings.json.");
builder.Services.AddSingleton(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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