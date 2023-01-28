using Autofac.Core;
using MediatR;
using Models;
using System.Reflection;
using TweetSampleApplication;
using TweetSampleApplication.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.Configure<TwitterApiCredential>(builder.Configuration.GetSection("TwitterApiCredential"));
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddHostedService<TweetSampleBackgroundService>();
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
