using Airnb.Application.Usecases.Bookings.CreateBooking;
using Airnb.Application.Usecases.Bookings.DeleteBooking;
using Airnb.Application.Usecases.Bookings.UpdateBooking;
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
            return services;
        }
    }
}
