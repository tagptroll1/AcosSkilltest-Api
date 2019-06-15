using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Acos.ProgrammingTask.Models;
using Acos.ProgrammingTask.Services;
using Acos.ProgrammingTask.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Acos.ProgrammingTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService; 
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings
        ){
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try {
                await _userService.Create(user, userDto.Password);
                return CreatedAtAction(nameof(GetById), new { id = userDto.Id}, userDto);
            }
            catch(UserException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserDto userDto)
        {
            var user = await _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return Ok( new {
                Id = user.UserId,
                Username = user.Username,
                Token = tokenString
            });
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);

            if (users == null)
                return NotFound();
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);

            if (user == null)
                return NotFound();
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.UserId = id;

            try
            {
                await _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch(UserException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

    }

}