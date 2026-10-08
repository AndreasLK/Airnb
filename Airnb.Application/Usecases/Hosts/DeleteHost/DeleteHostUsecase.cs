using Airnb.Application.Repository.Interfaces;
using Airnb.Application.Exceptions;

namespace Airnb.Application.Usecases.Hosts.DeleteHost
{
    public class DeleteHostUsecase : IDeleteHostUsecase
    {
        private readonly IHostRepository _hostRepository;
        private readonly IBookingRepository _bookingRepository;

        public DeleteHostUsecase(IHostRepository hostRepository, IBookingRepository bookingRepository)
        {
            _hostRepository = hostRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task ExecuteAsync(Guid hostId, CancellationToken cancellationToken = default)
        {
            // 1. Hosten skal findes
            var host = await _hostRepository.GetByIdAsync(hostId, cancellationToken)
                ?? throw new NotFoundException($"Host med ID {hostId} blev ikke fundet.");

            // 2. Man kan ikke stoppe som host, så længe man har bookinger i fremtiden
            var hasUpcomingBookings = await _bookingRepository.HasUpcomingForHostAsync(hostId, cancellationToken);
            if (hasUpcomingBookings)
                throw new ConflictException("Du kan ikke stoppe som host, så længe du har kommende bookinger.");

            // 3. Slet host-rollen – brugeren og gæsteprofilen bliver
            _hostRepository.Delete(host);
            await _hostRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
