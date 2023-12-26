using carvedrock_admin.Areas.Identity.Data;
using carvedrock_admin.Data;
using carvedrock_admin.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace carvedrock_admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContext
            builder.Services.AddDbContext<ProductContext>();
            builder.Services.AddDbContext<AdminContext>();

            //Add Identity
            builder.Services.AddDefaultIdentity<AdminUser>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = true;

                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;

                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                        options.Lockout.MaxFailedAccessAttempts = 5;
                        options.Lockout.AllowedForNewUsers = true;

                        options.User.AllowedUserNameCharacters =
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                        options.User.RequireUniqueEmail = false;
                    }
                ).AddEntityFrameworkStores<AdminContext>();
                
            builder.Services.AddScoped<ICarvedRockRepository, CarvedRockRepository>();
            builder.Services.AddScoped<IProductLogic, ProductLogic>();


            var app = builder.Build();

            //Add Migrations
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var ctx = services.GetRequiredService<ProductContext>();

                var userCtx = services.GetRequiredService<AdminContext>();
                userCtx.Database.Migrate();


                ctx.Database.Migrate();

                if (app.Environment.IsDevelopment())
                {
                    ctx.SeedInitialData();
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
