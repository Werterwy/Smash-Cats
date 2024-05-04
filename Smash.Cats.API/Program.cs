//using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Smash.Cats.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SmashContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
	c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

/*builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API WSVAP (WebSmartView)", Version = "v1" });
	c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
});*/

var app = builder.Build();

app.UseDeveloperExceptionPage();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

	app.UseDeveloperExceptionPage();
	app.UseSwagger(c =>
	{
		c.RouteTemplate = "/swagger/{documentName}/swagger.json";
	});
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
	/*	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json"
	});*/
}

app.UseAuthorization();

app.MapControllers();

app.Run();
