using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Airnb.Application.Repository.Interfaces;

namespace Airnb.Application.Usecases.Hosts.CreateHost
{
    public class BecomeHostUsecase : IBecomeHostUsecase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHostRepository _hostRepository;

        public BecomeHostUsecase(IUserRepository userRepository, IHostRepository hostRepository)
        {
            _userRepository = userRepository;
            _hostRepository = hostRepository;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // 1. Brugeren skal findes
            if (!await _userRepository.ExistsAsync(userId, cancellationToken))
                throw new KeyNotFoundException($"Bruger med ID {userId} blev ikke fundet.");

            // 2. Man kan kun være host én gang
            if (await _hostRepository.ExistsForUserAsync(userId, cancellationToken))
                throw new InvalidOperationException("Brugeren er allerede host.");

            // 3. Opret host-rollen
            var host = HostProfile.Create(userId);
            await _hostRepository.AddAsync(host, cancellationToken);
            await _hostRepository.SaveChangesAsync(cancellationToken);

            return host.Id;
        }
    }
}
