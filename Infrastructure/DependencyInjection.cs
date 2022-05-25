using Application.Common.Interfaces;
using ConsumeApi.Service;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
        
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)


                
            ));
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
            return services;
        }
    }
}