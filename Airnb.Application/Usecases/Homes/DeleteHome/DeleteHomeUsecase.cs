using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.Exceptions;

namespace Airnb.Application.Usecases.Homes.DeleteHome
{
    public class DeleteHomeUsecase : IDeleteHomeUsecase
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IBookingRepository _bookingRepository;

        public DeleteHomeUsecase(IHomeRepository homeRepository, IBookingRepository bookingRepository)
        {
            _homeRepository = homeRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task ExecuteAsync(Guid homeId, CancellationToken cancellationToken = default)
        {
            var home = await _homeRepository.GetByIdAsync(homeId, cancellationToken)
                ?? throw new KeyNotFoundException($"Hus med ID {homeId} blev ikke fundet.");

            var activeBookings = await _bookingRepository.GetByHomeIdAsync(homeId, cancellationToken);
            if (activeBookings.Any())
                throw new DomainException("Huset kan ikke slettes, da det har aktive bookinger.");

            _homeRepository.Delete(home);
            await _homeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}

