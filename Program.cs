using ContactManager.Data;
using ContactManager.Services;
using ContactManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserService, UserService>();

            var connectionString = builder.Configuration.GetConnectionString("ContactManagerContext");
            builder.Services.AddDbContext<ContactManagerContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=All}/{id?}");

            app.Run();
        }
    }
}