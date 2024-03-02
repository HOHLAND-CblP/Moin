using System.Collections.Specialized;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoinBackend.Domain;
using MoinBackend.Infrastructure;


class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        
        NameValueCollection appSetings = System.Configuration.ConfigurationManager.AppSettings;
        

        services.AddDomain();
        services.AddInfrastructure();
        services.AddControllers();

        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer("Bearer",options =>
            {
                /*options.TokenValidationParameters = new TokenValidationParameters
                {

                };*/
            });
        
        
        services.AddSwaggerGen();

        
        var app = builder.Build();

        app.UseAuthentication();
        
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