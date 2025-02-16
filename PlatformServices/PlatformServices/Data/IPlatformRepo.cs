using PlatformServices.Models;

namespace PlatformServices.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetPlatforms();
        Platform GetPlatformById(int id);

        void CreatePlatform(Platform plat);
    }
}
