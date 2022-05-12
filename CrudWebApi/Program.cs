using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

const string connectionString = @"Data Source=oleh-laptop;Initial Catalog=Users;Integrated Security=True";

builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();
