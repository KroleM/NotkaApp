using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotkaDatabaseContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("NotkaDatabaseContext") 
	?? throw new InvalidOperationException("Connection string 'NotkaDatabaseContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.UseAllOfToExtendReferenceSchemas();
});
//RepositoryWrapper
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

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
