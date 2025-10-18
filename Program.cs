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
using backend_01.Core.Category.Service;
using backend_01.Infrastructure.Category.Repository;




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
builder.Services.AddScoped<StaffRepository, StaffRepository>();
builder.Services.AddScoped<MenuRepository, MenuRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
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
    
    // THIS PART IS CRITICAL - Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//registering userService
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<CategoryService>();
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello From Asp.net Core Backend");
app.MapControllers();

//run the app at last
app.Run(); 