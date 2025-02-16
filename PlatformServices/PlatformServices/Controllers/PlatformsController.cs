using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformServices.Data;
using PlatformServices.Dtos;
using PlatformServices.Models;

namespace PlatformServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms ....");

            var platformItem = _platformRepo.GetPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id) { 
            
            var platformItem = _platformRepo.GetPlatformById(id);

            if (platformItem != null) { 
            
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            _platformRepo.CreatePlatform(platformModel);
            _platformRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new {Id =  platformReadDto.Id}, platformReadDto);
        }
    }
}
