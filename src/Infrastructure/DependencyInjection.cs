using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.LogTo(System.Console.WriteLine);
                    options.UseSqlServer(
                        connectionString,
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());



            return services;
        }
    }
}
