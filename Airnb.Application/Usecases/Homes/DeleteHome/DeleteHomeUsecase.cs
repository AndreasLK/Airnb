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

        public async Task ExecuteAsync(Guid homeId, Guid hostId, CancellationToken cancellationToken = default)
        {
            var home = await _homeRepository.GetByIdAsync(homeId, cancellationToken)
                ?? throw new KeyNotFoundException($"Hus med ID {homeId} blev ikke fundet.");

            if (home.HostId != hostId)
                throw new UnauthorizedAccessException("Du kan kun slette dine egne boliger.");  //sørger for at kun værten kan slette deres egne boliger

            if (await _bookingRepository.ExistsForHomeAsync(homeId, cancellationToken))
                throw new InvalidOperationException("Huset kan ikke slettes, da det har bookinger.");  //sørger for at boligen ikke kan slettes hvis der er bookinger på den

            _homeRepository.Delete(home);
            await _homeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}

