using ExpoCenter.Mvc.App_Start;
using ExpoCenter.Mvc.Data;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpoCenter.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var identityConnectionString = builder.Configuration.GetConnectionString("IdentityConnection") ??
                throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(identityConnectionString));

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            var expoCenterConnectionString = builder.Configuration.GetConnectionString("ExpoCenterConnection") ??
                throw new InvalidOperationException("Connection string 'ExpoCenterConnection' not found.");
            builder.Services.AddDbContext<ExpoCenterDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(expoCenterConnectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(IdentityConfig.SetIdentityOptions())
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(o => o.AddPolicy("ParticipantesExcluir", Policies.ParticipantesExcluirPolicy));

            builder.Services.AddControllersWithViews();

            builder.Logging.AddLog4Net("log4net.config");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}