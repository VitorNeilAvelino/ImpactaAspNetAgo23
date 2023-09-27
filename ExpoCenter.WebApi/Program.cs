using ExpoCenter.Repositorios.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ExpoCenter.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var expoCenterConnectionString = builder.Configuration.GetConnectionString("ExpoCenterConnection") ??
                throw new InvalidOperationException("Connection string 'ExpoCenterConnection' not found.");
            
            builder.Services.AddDbContext<ExpoCenterDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(expoCenterConnectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(c => c
                .AllowAnyHeader()
                //.WithHeaders(HeaderNames.Authorization, "user-email")
                .AllowAnyMethod()
                //.WithMethods("get", "POST")
                //.AllowAnyOrigin()
                .WithOrigins("http://localhost:5037")
                );

            app.Run();
        }
    }
}