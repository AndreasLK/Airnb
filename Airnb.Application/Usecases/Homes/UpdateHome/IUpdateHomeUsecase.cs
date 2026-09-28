using Airnb.Shared.Homes.Requests;


namespace Airnb.Application.Usecases.Homes.UpdateHome
{
    public interface IUpdateHomeUsecase
    {
        Task ExecuteAsync(Guid homeId, UpdateHomeRequest request, CancellationToken cancellationToken = default);
    }
}
