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
    public class PostitController : ControllerBase
    {
        private IWhiteboardService _boardService;
        private IUserService _userService;
        private IPostitService _postitService;
        private IMapper _mapper;

        public PostitController(
            IPostitService service, 
            IWhiteboardService boardService,
            IUserService userService,
            IMapper mapper)
        {
            _postitService = service;
            _boardService = boardService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var note = await _postitService.GetAll();
            var notes = _mapper.Map<List<PostitDtoIn>>(note);

            if (notes.Count == 0)
                return NotFound();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await _postitService.GetById(id);
            var noteDto = _mapper.Map<PostitDtoIn>(note);

            if (note == null)
                return NotFound();
            return Ok(noteDto);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var notes = await _userService.GetAllPostits(id);
            var notesDto = _mapper.Map<List<PostitDtoIn>>(notes);

            if (notesDto.Count == 0)
                return NotFound();
            return Ok(notesDto);
        }

        [HttpGet("whiteboard/{id}")]
        public async Task<IActionResult> GetByWhiteboardId(int id)
        {
            var notes = await _boardService.GetAllPostits(id);
            var NotesDto = _mapper.Map<List<PostitDtoIn>>(notes);

            if (NotesDto.Count == 0)
                return NotFound();
            return Ok(NotesDto);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create(PostitDtoIn postit)
        {
            if (postit.Whiteboard == null)
                return BadRequest("A whiteboard must be provided to add a note to");
            var note = _mapper.Map<Postit>(postit);

            await _postitService.Create(note);

            return CreatedAtAction(nameof (GetById), new { id = postit.Id}, postit);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PostitDtoIn postit)
        {
            if (postit.Whiteboard == null)
                return BadRequest("Whiteboard must be given");

            var note = _mapper.Map<Postit>(postit);
            await _postitService.Update(note);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _postitService.Delete(id);
            return Ok();
        }

    }
}

// Newtonsoft.Json.JsonSerializationException: 
// Self referencing loop detected for property 'postit' 
// with type 'Acos.ProgrammingTask.Models.Postit'. Path 'todo'.
