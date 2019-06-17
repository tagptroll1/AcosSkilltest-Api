using System.Collections.Generic;
using System.Threading.Tasks;
using Acos.ProgrammingTask.Models;
using Acos.ProgrammingTask.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acos.ProgrammingTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ReportController : ControllerBase
    {
        private IWhiteboardService _boardService;
        private IUserService _userService;
        private IPostitService _postitService;
        private IMapper _mapper;

        public ReportController(IMapper mapper, IWhiteboardService board, IUserService user, IPostitService postit)
        {
            _mapper = mapper;
            _boardService = board;
            _userService = user;
            _postitService = postit;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getAllFromUser(int id)
        {
            var user = await _userService.GetById(id);
            var userDto = _mapper.Map<ReportUserDto>(user);

            return Ok(userDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAll();
            var userDto = _mapper.Map<List<ReportUserDto>>(user);

            return Ok(userDto);
        }
    }
}