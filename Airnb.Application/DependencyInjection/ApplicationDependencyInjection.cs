using Airnb.Application.Usecases.Bookings.CreateBooking;
using Airnb.Application.Usecases.Bookings.DeleteBooking;
using Airnb.Application.Usecases.Bookings.UpdateBooking;
using Airnb.Application.Usecases.Homes.CreateHome;
using Airnb.Application.Usecases.Homes.DeleteHome;
using Airnb.Application.Usecases.Homes.UpdateHome;
using Airnb.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;


namespace Airnb.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateBookingUsecase, CreateBookingUsecase>();
            services.AddScoped<IUpdateBookingUsecase, UpdateBookingUsecase>();
            services.AddScoped<IDeleteBookingUsecase, DeleteBookingUsecase>();

            services.AddScoped<ICreateHomeUsecase, CreateHomeUsecase>();
            services.AddScoped<IUpdateHomeUsecase, UpdateHomeUsecase>();
            services.AddScoped<IDeleteHomeUsecase, DeleteHomeUsecase>();

            services.AddScoped<IBookingConflictChecker, BookingConflictChecker>();
            return services;
        }
    }
}
