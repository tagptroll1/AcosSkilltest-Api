using System.Collections.Generic;
using System.Threading.Tasks;
using Acos.ProgrammingTask.Models;
using Acos.ProgrammingTask.Services;
using Acos.ProgrammingTask.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acos.ProgrammingTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class WhiteboardController : ControllerBase
    {
        private IWhiteboardService _boardService;
        private IUserService _userService;
        private IMapper _mapper;



        public WhiteboardController(IWhiteboardService boardService, IMapper mapper, IUserService userservice)
        {
            _boardService = boardService;
            _userService = userservice;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var board = await _boardService.GetWhiteboardById(id);
            var boardDto = _mapper.Map<WhiteboardDtoOut>(board);

            if (board == null)
                return NotFound();
            return Ok(boardDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var boards = await _boardService.GetAll();
            var boardDtos = _mapper.Map<List<WhiteboardDtoOut>>(boards);
            if (boards == null)
                return NotFound();
            return Ok(boardDtos);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetAllFromUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound("User does not exist");

            var boards = await _boardService.GetAllFromUser(user);
            var boardDtos = _mapper.Map<List<WhiteboardDtoOut>>(boards);

            if (boards.Count == 0)
            {
                return NotFound("Found no whiteboards associated with this user");
            }
            return Ok(boardDtos);
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateNew(WhiteboardDtoIn boardDto)
        {
            if (boardDto.Owner == null)
                return BadRequest(new { message = "You must provide a Owner id, email or username to the Owner field" });

            var owner = await _userService.JustFindHim(boardDto.Owner);

            if (owner == null)
                return BadRequest("Could not find User associated to the Owner field");

            var board = new Whiteboard()
            {
                Id = boardDto.Id,
                Title = boardDto.Title,
                Owner = owner
            };

            try
            {
                await _boardService.CreateWhiteboard(board);
                return CreatedAtAction(nameof(GetById), new { id = boardDto.Id }, boardDto);
            }
            catch (WhiteboardException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]WhiteboardDtoIn boardIn)
        {
            var board = _mapper.Map<Whiteboard>(boardIn);
            try
            {
                await _boardService.UpdateWhiteboard(board);
                return Ok();
            }
            catch (WhiteboardException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _boardService.DeleteWhiteboard(id);
            return Ok();
        }
    }
}