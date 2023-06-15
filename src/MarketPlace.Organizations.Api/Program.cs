using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json", false, false);
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.

	app.UseSwagger();
	app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(cors=>
{
	cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
