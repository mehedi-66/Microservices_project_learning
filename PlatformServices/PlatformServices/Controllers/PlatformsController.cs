using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformServices.Data;
using PlatformServices.Dtos;
using PlatformServices.Models;
using PlatformServices.SyncDataServices.Http;

namespace PlatformServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(ICommandDataClient commandDataClient, IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
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
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            _platformRepo.CreatePlatform(platformModel);
            _platformRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex) { 
                Console.WriteLine($" --> Could not send Synchronously: { ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new {Id =  platformReadDto.Id}, platformReadDto);
        }
    }
}
