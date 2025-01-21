using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using colaboradores.Data;

namespace colaboradores
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // L� a string de conex�o do appsettings.json ou vari�vel de ambiente para Docker
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40)); // Vers�o do MySQL

            // Configura o DbContext com MySQL
            builder.Services.AddDbContext<TesteContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            // Configura��o do Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TesteContext>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configura��o do pipeline de requisi��o
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
