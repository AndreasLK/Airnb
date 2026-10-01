using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Guests.Request;

namespace Airnb.Application.Usecases.Guests.UpdateGuest
{
    public class UpdateGuestUsecase : IUpdateGuestUsecase
    {
        private readonly IGuestRepository _guestRepository;

        public UpdateGuestUsecase(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task ExecuteAsync(Guid guestId, UpdateGuestRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Find guesten – findes den ikke, bliver det til 404 via GlobalExceptionHandler
            var guest = await _guestRepository.GetByIdAsync(guestId, cancellationToken)
                ?? throw new KeyNotFoundException($"Guest med ID {guestId} blev ikke fundet.");

            // 2. Lad domænet selv opdatere sig
            guest.UpdateAddress(new Address(
                request.Country,
                request.PostalCode,
                request.City,
                request.StreetName,
                request.HouseNumber,
                request.Floor));

            // 3. Gem
            _guestRepository.Update(guest);
            await _guestRepository.SaveChangesAsync(cancellationToken);
        }

    }
}

