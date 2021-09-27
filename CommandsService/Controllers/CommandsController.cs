using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers {
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform {platformId}");
            if (!_repository.PlatformExists(platformId))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(_repository.GetCommandsForPlatform(platformId)));
        }
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId) {
            Console.WriteLine($"--> Hit GetCommandForPlatform {platformId} / command: {commandId}");
            if (!_repository.PlatformExists(platformId)) {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(_repository.GetCommand(platformId, commandId)));
        }
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto) {
            Console.WriteLine($"--> Hit CreateCommandForPlatform {platformId} ");
            if (!_repository.PlatformExists(platformId)) {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new {platformId = commandReadDto.PlatformId, commandId = commandReadDto.Id}, commandReadDto);

        }

    }
}
