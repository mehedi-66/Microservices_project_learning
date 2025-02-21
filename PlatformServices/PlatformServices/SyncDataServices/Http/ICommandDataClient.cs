using PlatformServices.Dtos;

namespace PlatformServices.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platform);
    }
}
