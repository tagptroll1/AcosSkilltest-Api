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
            var notes = _mapper.Map<List<Postit>>(note);

            if (notes.Count == 0)
                return NotFound();
            return Ok(note);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await _postitService.GetById(id);
            var noteDto = _mapper.Map<Postit>(note);

            if (note == null)
                return NotFound();
            return Ok(noteDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            var notes = await _userService.GetAllPostits(userid);
            var notesDto = _mapper.Map<List<Postit>>(notes);

            if (notesDto.Count == 0)
                return NotFound();
            return Ok(notesDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetByWhiteboardId(int boardId)
        {
            var notes = await _boardService.GetAllPostits(boardId);
            var NotesDto = _mapper.Map<List<Postit>>(notes);

            if (NotesDto.Count == 0)
                return NotFound();
            return Ok(NotesDto);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create(PostitDtoIn postit)
        {
            var board = await _boardService.GetById(postit.WhiteboardId);
            var todo = _mapper.Map<Todo>(postit.Todo);
            var note = _mapper.Map<Postit>(postit);
            //note.Todo = todo;

            await _postitService.Create(note);

            return CreatedAtAction(nameof (GetById), new { id = note.Id}, note);
        }

    }
}

// Newtonsoft.Json.JsonSerializationException: 
// Self referencing loop detected for property 'postit' 
// with type 'Acos.ProgrammingTask.Models.Postit'. Path 'todo'.
