using ExpoCenter.Repositorios.SqlServer;
using ExpoCenter.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;

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

            var identityConnectionString = builder.Configuration.GetConnectionString("IdentityConnection") ??
                throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(identityConnectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
                        ClockSkew = TimeSpan.Zero
                    }) ;

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExpoCenter.WebApi", Version = "v1" });

                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

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