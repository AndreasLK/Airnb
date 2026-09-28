using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Infrastructure.Queries.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Airnb.Application.Repository.Interfaces;
using Airnb.Infrastructure.Repositories.Bookings;



namespace Airnb.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AirnbDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBookingQueries, BookingQueries>();
            services.AddScoped<IBookingRepository, BookingRepository>();  //det skal åbenbart være her, ellers kan man ikke slette en booking, da den ikke kan findes i databasen

            return services;
        }
    }
}
