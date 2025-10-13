using backend_01.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
//create a builder first
var builder = WebApplication.CreateBuilder(args);

//inject the services after builder is created and before app instacnce is created.


//this connects the db and the application with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//this is for the documentation
builder.Services.AddOpenApi();

//build app
var app = builder.Build();

//useMiddlewares and sequence matters.
app.UseHttpsRedirection();


//run the app at last
app.Run();