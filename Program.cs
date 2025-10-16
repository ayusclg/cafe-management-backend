using Microsoft.OpenApi.Models; 
using backend_01.Core.User.Service;
using backend_01.Infrastructure.Data;
using backend_01.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using backend_01.Infrastructure.Menu.Repository;
using backend_01.Core.Menu.Service;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend_01.Infrastructure.Token.Service;



//create a builder first
var builder = WebApplication.CreateBuilder(args);

//inject the services after builder is created and before app instance is created.


 
//register controller support 
 
builder.Services.AddControllers() 
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//this connects the db and the application with connection string



//this is the jwt that verify the incoming token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:iss"],
        ValidAudience = builder.Configuration["Jwt:aud"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();
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
builder.Services.AddScoped<TokenService>();
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
        c.RoutePrefix = "swagger";
    });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapGet("/", () => "Hello From Asp.net Core Backend");
app.MapControllers();

//run the app at last
app.Run(); 