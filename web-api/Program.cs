using web_api.Data;
using web_api.Services;
using web_api.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefualtConnecton");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddScoped<IUserRepository, UserReposiotry>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
