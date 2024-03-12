using System.Text;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoinBackend.Domain;
using MoinBackend.Domain.Services;
using MoinBackend.Domain.Settings;
using MoinBackend.Infrastructure;
using MoinBackend.Infrastructure.Settings;


class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;

        services.Configure<AuthSettings>(config.GetSection("JWT"));
        services.Configure<DbsOptions>(config.GetSection("DataBases"));

        services.AddDomain();
        services.AddDbInfrastructure();
        services.AddControllers();

        var issuer = config["JWT:Issuer"];
        var audience = config["JWT:Audience"];
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWT:SecretKey"]));

        services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                };
            });


        services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

    var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.MapGet("/", () => "Hello, it`s Moin!");

        app.Run();
    }
}