using Airnb.Application.Usecases.Bookings.CreateBooking;
using Airnb.Application.Usecases.Bookings.DeleteBooking;
using Airnb.Application.Usecases.Bookings.UpdateBooking;
using Airnb.Application.Usecases.Guests.UpdateGuest;
using Airnb.Application.Usecases.Homes.CreateHome;
using Airnb.Application.Usecases.Homes.DeleteHome;
using Airnb.Application.Usecases.Homes.UpdateHome;
using Airnb.Application.Usecases.Users.Login;
using Airnb.Application.Usecases.Users.RefreshTokens;
using Airnb.Application.Usecases.Users.RegisterUser;
using Airnb.Application.Usecases.Hosts.CreateHost;
using Airnb.Application.Usecases.Hosts.DeleteHost;
using Airnb.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;


namespace Airnb.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Booking Usecases
            services.AddScoped<ICreateBookingUsecase, CreateBookingUsecase>();
            services.AddScoped<IUpdateBookingUsecase, UpdateBookingUsecase>();
            services.AddScoped<IDeleteBookingUsecase, DeleteBookingUsecase>();

            //Home Usecases
            services.AddScoped<ICreateHomeUsecase, CreateHomeUsecase>();
            services.AddScoped<IUpdateHomeUsecase, UpdateHomeUsecase>();
            services.AddScoped<IDeleteHomeUsecase, DeleteHomeUsecase>();

            //Guest Usecases
            services.AddScoped<IUpdateGuestUsecase, UpdateGuestUsecase>();

            //Domain Services
            services.AddScoped<IBookingConflictChecker, BookingConflictChecker>();

            //User Usecases
            services.AddScoped<ILoginUsecase, LoginUsecase>();
            services.AddScoped<IRegisterUserUsecase, RegisterUserUsecase>();
            services.AddScoped<IRefreshTokenUsecase, RefreshTokenUsecase>();

            //Host Usecases
            services.AddScoped<IBecomeHostUsecase, BecomeHostUsecase>();
            services.AddScoped<IDeleteHostUsecase, DeleteHostUsecase>();

            return services;
        }
    }
}
