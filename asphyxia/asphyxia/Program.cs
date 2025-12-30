using asphyxia;
using asphyxia.Formatters;
using asphyxia.Models;
using Formatters.Formatters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<HostConfig>(builder.Configuration.GetSection("asphyxia.host"));
builder.Services.AddDbContext<AsphyxiaContext>(options =>
{
    options.EnableSensitiveDataLogging();
});
builder.Services.AddMvc(options =>
{

    options.InputFormatters.Insert(0, new EamuseXrpcInputFormatter());
    options.OutputFormatters.Insert(0, new EamuseXrpcOutputFormatter());
    options.EnableEndpointRouting = false;
    options.Conventions.Add(new XrpcCallConvention());
});

/* WebHost configuration */
builder.WebHost.UseUrls(config.GetSection("asphyxia.host").GetValue<string>("host_url"));


var app = builder.Build();
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
using (var context = serviceScope.ServiceProvider.GetRequiredService<AsphyxiaContext>())
{
    context.Database.Migrate();

    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    Console.WriteLine($"[{context.Connection.RemoteIpAddress}] | {context.Request.Method} | {context.Request.Path}{context.Request.QueryString}");
    await next.Invoke();
});

app.UseStatusCodePages(async (StatusCodeContext context) => {
    var response = context.HttpContext.Response;
    var request = context.HttpContext.Request;

    Console.WriteLine($"[{context.HttpContext.Connection.RemoteIpAddress}] | {request.Method} | {request.Path}{request.QueryString} - {response.StatusCode}");
});

app.UseMvc();
app.Run();
