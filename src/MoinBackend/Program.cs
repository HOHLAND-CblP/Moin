using System.Text;
using FluentMigrator.Runner;
using FluentMigrator.Runner.BatchParser.RangeSearchers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoinBackend.Domain;

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
        

        services.AddDomain(config);
        services.AddDbInfrastructure(config);
        //services.AddApiVersioning();
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
        
        if (args.Length > 0 && args[0].Equals("migrate", StringComparison.InvariantCultureIgnoreCase))
        {
            using var scope = app.Services.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            switch (args[1])
            {
                case "Up":
                    runner.MigrateUp();
                    break;
                case "Down":
                    runner.MigrateDown(Int32.Parse(args[2]));
                    break;
                default:
                    Console.WriteLine("Invalid second argument. Please write \"Up\" or \"Down\"");
                    break;
            }
            
            return;
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            Console.WriteLine("Development");
        }

        app.MapControllers();
        app.MapGet("/", () => "Hello, it`s Moin!");

        app.Run();
    }
}