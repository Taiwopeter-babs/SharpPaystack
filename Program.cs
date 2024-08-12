using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SharpPayStack.Extensions;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApiClients();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureDatabaseContext(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureIdentity();
builder.Services.AddAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
