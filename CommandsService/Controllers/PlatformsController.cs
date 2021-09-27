using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers {
    [ApiController]
    [Route("api/c/[Controller]")]
    public class PlatformsController : ControllerBase{
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from CommandService");
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAllPlatforms()));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Indound POST # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
