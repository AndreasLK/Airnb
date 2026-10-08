using Airnb.Application.Exceptions.Airnb.Application.Exceptions;
using Airnb.Application.Repository.Interfaces;
using Airnb.Application.Security.Interfaces;
using Airnb.Domain.Entities;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Users.Requests;


namespace Airnb.Application.Usecases.Users.RegisterUser
{
    public class RegisterUserUsecase : IRegisterUserUsecase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserUsecase(IUserRepository userRepository, IGuestRepository guestRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _guestRepository = guestRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Check if the email is already registered
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
                throw new ConflictException($"Email {request.Email} is already registered.");

            // 2. Opret brugeren med et hashet password
            var user = User.Create(request.Email, request.Name, _passwordHasher.Hash(request.Password)); //her hasher vi passwordet, så det ikke bliver gemt i klar tekst i databasen

            // 3. Alle brugere bliver automatisk gæster
            var address = new Address(
                request.Country,
                request.PostalCode,
                request.City,
                request.StreetName,
                request.HouseNumber,
                request.Floor);
            var guest = GuestProfile.Create(user.Id, address);

            // 4. Læg begge på huskelisten (intet er skrevet til databasen endnu)
            await _userRepository.AddAsync(user, cancellationToken);
            await _guestRepository.AddAsync(guest, cancellationToken);

            // 5. Gem det hele i én transaktion: enten kommer begge ind, eller ingen af dem
            await _userRepository.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
