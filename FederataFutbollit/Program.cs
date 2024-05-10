using System.Text;
using FederataFutbollit.Contracts;
using FederataFutbollit.Data;
using FederataFutbollit.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//ADD Identity & JWT AUTHENTICATION 

//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<DataContext>()
.AddSignInManager()
.AddRoles<IdentityRole>();

//JWT
builder.Services.AddAuthentication(Options => 
{
Options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
Options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( options =>
{
options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))


                };


});

builder.Services.AddSwaggerGen(Options =>
{
    Options.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme
    
    
    {
        In= ParameterLocation.Header,
        Name= "Authorization",
        Type=SecuritySchemeType.ApiKey

    });
    Options.OperationFilter<SecurityRequirementsOperationFilter>();

});
 builder.Services.AddScoped<IUserAccount, AccountRepository>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
