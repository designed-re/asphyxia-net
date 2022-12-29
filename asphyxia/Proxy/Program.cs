using System.Linq;
using System.Text.Json;
using AspNetCore.Proxy;
using Proxy.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProxies();

var app = builder.Build();


app.RunProxy(proxy =>
{
    proxy.UseHttp(x =>
    {
        x.WithEndpoint("http://localhost:8083");
        x.WithOptions(opt =>
        {
            opt.WithAfterReceive(async (context, next) =>
            {
                try
                {
                    Console.WriteLine(string.Join("",context.Request.Headers.Select(x => $"{x.Key}:{x.Value}, ")));
                    var i = await new EamuseXrpcInputFormatter().TestRead(context);
                    Console.WriteLine(((EamuseXrpcData)i.Model).Document);
                }
                catch (Exception e)
                {
                    Console.WriteLine("maybe incorrect request");
                    Console.WriteLine(e.ToString());
                }
            });
        });
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();
