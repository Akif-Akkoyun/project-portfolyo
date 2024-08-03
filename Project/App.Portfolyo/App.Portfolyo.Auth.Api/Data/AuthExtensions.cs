using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolyoApp.Data.Infrastructure;
using System;

namespace PortfolyoApp.Auth.Api.Data
{
    public static class AuthExtensions
    {
        public static void AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DbContext, AuthDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
