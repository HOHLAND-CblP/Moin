using System.Collections.Specialized;
using Microsoft.IdentityModel.Tokens;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        
        NameValueCollection appSetings = System.Configuration.ConfigurationManager.AppSettings;
        
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                    };
                }
                );
        
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