using Airnb.Shared.Guests.Request;

namespace Airnb.Application.Usecases.Guests.UpdateGuest
{
    public interface IUpdateGuestUsecase
    {
        Task ExecuteAsync(Guid guestId, UpdateGuestRequest request, CancellationToken cancellationToken = default);
    }
}
