using luna;
using luna.Models;
using luna.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using luna.Utils.Formatters;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.

string path = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
var files = Directory.GetFiles(path, "*.dll");

foreach (var file in files)
{
    Console.WriteLine("Loading modules... {0}", Path.GetFileName(file));
    var assembly = Assembly.Load(File.ReadAllBytes(file)); //LoadFile lock file.
    builder.Services.AddControllers().AddApplicationPart(assembly);
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<HostConfig>(builder.Configuration.GetSection("luna.host"));
builder.Services.AddDbContext<AsphyxiaContext>(options =>
{
        options.UseMySql(config.GetSection("ea.host")["mariadb_connstr"],
            new MariaDbServerVersion("12.1.2-mariadb"));
    
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
builder.WebHost.UseUrls(config.GetSection("ea.host").GetValue<string>("host_url") ?? "http://+:8083");
builder.Services.AddSingleton(config);


var app = builder.Build();
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
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
