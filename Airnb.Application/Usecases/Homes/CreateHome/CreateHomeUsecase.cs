using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.Entities;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Homes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Homes.CreateHome
{
    public class CreateHomeUsecase : ICreateHomeUsecase
    {
        private readonly IHomeRepository _homeRepository;

        public CreateHomeUsecase(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        public async Task<HomeDto> ExecuteAsync(CreateHomeRequest request, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<HomeType>(request.HomeType, ignoreCase: true, out var homeType))
                throw new ArgumentException($"Ugyldig boligtype: {request.HomeType}");

            var home = Home.Create(
                request.HostId,
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
                request.PartiesAllowed,
                request.Capacity));

            await _homeRepository.AddAsync(home, cancellationToken);
            await _homeRepository.SaveChangesAsync(cancellationToken);

            return new HomeDto(
                home.Id,
                home.HostId,
                home.Capacity,
                home.Address.City,
                home.HomeType.ToString(),
                home.PricePerDay.Amount,
                home.PricePerDay.Currency);
        }
    }
}

