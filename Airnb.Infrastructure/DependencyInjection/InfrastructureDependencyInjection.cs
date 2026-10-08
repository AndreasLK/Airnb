using Airnb.Application.Repository.Interfaces;
using Airnb.Application.Security.Interfaces;
using Airnb.Domain.DomainServices;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Infrastructure.Queries.Bookings;
using Airnb.Infrastructure.Queries.Guests;
using Airnb.Infrastructure.Queries.Homes;
using Airnb.Infrastructure.Queries.Hosts;
using Airnb.Infrastructure.Repositories.Bookings;
using Airnb.Infrastructure.Repositories.Guests;
using Airnb.Infrastructure.Repositories.Homes;
using Airnb.Infrastructure.Repositories.Host;
using Airnb.Infrastructure.Repositories.RefreshTokens;
using Airnb.Infrastructure.Repositories.Users;
using Airnb.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airnb.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AirnbDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Booking Queries and Repositories
            services.AddScoped<IBookingQueries, BookingQueries>();
            services.AddScoped<IBookingRepository, BookingRepository>();  //det skal åbenbart være her, ellers kan man ikke slette en booking, da den ikke kan findes i databasen

            //Home Queries and Repositories
            services.AddScoped<IHomeQueries, HomeQueries>();
            services.AddScoped<IHomeRepository, HomeRepository>();

            //Guest Queries and Repositories
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IGuestQueries, GuestQueries>();

            //Host Queries and Repositories
            services.AddScoped<IHostRepository, HostRepository>();
            services.AddScoped<IHostQueries, HostQueries>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();  // Singleton, fordi det ikke har nogen tilstand, og vi ønsker at genbruge den samme instans på tværs af hele applikationen.
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            
            

            return services;
        }
    }
}
