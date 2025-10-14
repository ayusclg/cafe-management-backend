using Microsoft.OpenApi.Models;
using backend_01.Core.User.Service;
using backend_01.Infrastructure.Data;
using backend_01.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using backend_01.Infrastructure.Menu.Repository;
using backend_01.Core.Menu.Service;



//create a builder first
var builder = WebApplication.CreateBuilder(args);

//inject the services after builder is created and before app instacnce is created.



//register controller support 
builder.Services.AddControllers();

//this connects the db and the application with connection string


builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<MenuRepository, MenuRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//swagger implementation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Backend_01 API",
        Version = "v1",
        Description = "User Management API with PostgreSQL"
    });
});

//registering userService
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MenuService>();
//this is for the documentation


//build app
var app = builder.Build();

//useMiddlewares and sequence matters.
app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend_01 API v1");
        c.RoutePrefix = string.Empty;
    });
app.UseHttpsRedirection();
app.MapGet("/", () => "Hello From Asp.net Core Backend");
app.MapControllers();

//run the app at last
app.Run();