using System.Text.Json.Serialization;
using BeecareDeploy.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var a = builder.Configuration.Get<DeployConfig>();
var config = JsonConvert.DeserializeObject<DeployConfig?>(builder.Configuration.GetSection("DeployConfig").Value.ToString());
if (config is null) throw new Exception("Deploy configuration was not found. Check the appsettings.json.");
builder.Services.AddSingleton(config);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();