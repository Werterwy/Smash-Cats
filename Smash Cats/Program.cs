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
using Microsoft.EntityFrameworkCore;
using Smash_Cat;
using Smash_Cats.Models;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SmashCatsContext>(
    options => options.UseSqlServer(connectionString));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var cultures = new[]
	{
		new CultureInfo("ru-Ru"),
		new CultureInfo("kz-Kz"),
		new CultureInfo("en-Us")
	};

	options.DefaultRequestCulture = new RequestCulture("ru-Ru", "ru-Ru");
	options.SupportedCultures = cultures;
	options.SupportedUICultures = cultures;

});

builder.Services.AddHttpClient();

builder.Services.AddLocalization(option =>
option.ResourcesPath = "Resources");

builder.Host.UseSerilog(Log.Logger);

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.AddHttpContextAccessor();

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
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = ".SmachCats.Session";
});


builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
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

//app.UseMiddleware<ContentMiddleware>();


var localOptios = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(localOptios.Value);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
