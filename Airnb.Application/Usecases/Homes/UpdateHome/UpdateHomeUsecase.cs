using Airnb.Application.Repository.Interfaces;
using Airnb.Shared.Homes.Requests;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;


namespace Airnb.Application.Usecases.Homes.UpdateHome
{
    public class UpdateHomeUsecase : IUpdateHomeUsecase
    {
        private readonly IHomeRepository _homeRepository;

        public UpdateHomeUsecase(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task ExecuteAsync(Guid homeId, UpdateHomeRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Find huset – findes det ikke, bliver det til 404 via GlobalExceptionHandler
            var home = await _homeRepository.GetByIdAsync(homeId, cancellationToken)
                ?? throw new KeyNotFoundException($"Hus med ID {homeId} blev ikke fundet.");

            // 2. Oversæt tekst til enum – samme som i CreateHomeUsecase
            if (!Enum.TryParse<HomeType>(request.HomeType, ignoreCase: true, out var homeType))
                throw new ArgumentException($"Ugyldig boligtype: {request.HomeType}");

            // 3. Lad domænet selv opdatere sig (og validere via Validate())
            home.UpdateDetails(
                request.Capacity,
                new Address(
                    request.Country,
                    request.PostalCode,
                    request.City,
                    request.StreetName,
                    request.HouseNumber,
                    request.Floor),
                request.CheckInTime,
                request.CheckOutTime,
                homeType,
                new Money(request.PricePerDay, request.Currency),
                new HomeRules(
                    request.PetsAllowed,
                    request.SmokingAllowed,
                    request.PartiesAllowed));

            // 4. Gem
            await _homeRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
