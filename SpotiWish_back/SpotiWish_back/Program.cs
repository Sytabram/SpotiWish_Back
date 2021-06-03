using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpotiWish_back.Data;
using SpotiWish_back.Model;

namespace SpotiWish_back
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host= CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SpotiWishDataContext>();
                    await context.Database.MigrateAsync();

                    var usersManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await rolesManager.RoleExistsAsync("admin"))
                    {
                        var user = new User()
                        {
                            UserName = "Selmir",
                            Email = "selmir@epsic.ch",
                            EmailConfirmed = true,

                        };
                        await usersManager.CreateAsync(user, "Ep$icCovid2021");
                        var adminRole = await rolesManager.CreateAsync(new IdentityRole("admin"));
                        var userRole = await rolesManager.CreateAsync(new IdentityRole("user"));
                        await usersManager.AddClaimAsync(user, new Claim("IsMedecin", "false"));

                        await usersManager.AddToRoleAsync(user, "admin");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}