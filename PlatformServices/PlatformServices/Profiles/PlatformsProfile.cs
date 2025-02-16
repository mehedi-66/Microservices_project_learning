using AutoMapper;
using PlatformServices.Dtos;
using PlatformServices.Models;

namespace PlatformServices.Profiles
{
    public class PlatformsProfile: Profile
    {
        public PlatformsProfile()
        {
            // Source obj (Platform) -> Target (Read Platform)
            CreateMap<Platform, PlatformReadDto>();

            // Source (Create Platform) -> Target (Platform)
            CreateMap<PlatformCreateDto, Platform>();
            
        }
    }
}
