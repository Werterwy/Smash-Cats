using Serilog;
using Seq.Extensions.Logging;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DemoSeriLogDB");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5165/")
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
     /* .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }, null, null,
          LogEventLevel.Information, null, null, null, null)*/
    .CreateLogger();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Host.UseSerilog(Log.Logger);

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.Configure<CookieTempDataProviderOptions>
    (options =>
    {
        options.Cookie.IsEssential = true;
        options.Cookie.Domain = "localhost:5165";
        options.Cookie.Expiration =
        TimeSpan.FromSeconds(160);
    });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.Name = ".SmachCats.Session";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
