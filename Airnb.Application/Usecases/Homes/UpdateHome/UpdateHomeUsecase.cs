using Airnb.Application.Repository.Interfaces;
using Airnb.Shared.Homes.Requests;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;
using Airnb.Application.Exceptions;


namespace Airnb.Application.Usecases.Homes.UpdateHome
{
    public class UpdateHomeUsecase : IUpdateHomeUsecase
    {
        private readonly IHomeRepository _homeRepository;

        public UpdateHomeUsecase(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task ExecuteAsync(Guid homeId, Guid hostId, UpdateHomeRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Find huset – findes det ikke, bliver det til 404 via GlobalExceptionHandler
            var home = await _homeRepository.GetByIdAsync(homeId, cancellationToken)
                ?? throw new NotFoundException($"Hus med ID {homeId} blev ikke fundet.");

            //2. Tjek at det er den rigtige host, der prøver at ændre boligen
            if (home.HostProfileId != hostId)
                throw new ForbiddenException("Du kan kun ændre dine egne boliger.");

            // 3. Tjek at HomeType er gyldig
            if (!Enum.TryParse<HomeType>(request.HomeType, ignoreCase: true, out var homeType))
                throw new ValidationException($"Ugyldig boligtype: {request.HomeType}");



            // 4. Opdater boligen med de nye detaljer
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
                    new Money(
                    request.PricePerDay, 
                    request.Currency),
                    new HomeRules(
                    request.PetsAllowed,
                    request.SmokingAllowed,
                    request.PartiesAllowed));

            // 5. Gem
            await _homeRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
