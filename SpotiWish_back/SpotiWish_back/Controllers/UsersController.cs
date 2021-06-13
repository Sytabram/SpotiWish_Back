using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpotiWish_back.Configuration;
using SpotiWish_back.Model;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService usersService,IAuthorizationService authorizationService, IMapper mapper, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _logger = logger;
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet("User")]
        public async Task<IActionResult> GetAllUser()
        {   
            var Users = await _usersService.GetAllUser();
            return Ok(_mapper.Map<List<User>, List<UserDTO>>(Users));
        }
        
        [Authorize(Roles = "user, admin")]
        [HttpPost("User/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDUserDTO UserToEdit)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");
            if (authorizationResult.Succeeded)
            {
                var modelDb = await _usersService.UpdateUser(id, UserToEdit);

                var modelDto = _mapper.Map<UserDTO>(modelDb);
                return Ok(modelDto);
            }
            else
                return Forbid();
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetSingleUser([FromRoute] int id)
        {   
            
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");
            if (authorizationResult.Succeeded)
            {
                var singleUser = await _usersService.GetSingleUser(id);
                
                if (singleUser == null)
                    return NotFound();
                
                return Ok(_mapper.Map<UserDTO>(singleUser));
            }
           
            else
                return Forbid();
        }
        
        [Authorize(Roles = "user, admin")]
        [HttpDelete("User/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");
            if (authorizationResult.Succeeded)
            {
                await _usersService.DeleteUser(id);
                return NoContent();
            }
            else
                return Forbid();
        }
        
        [Authorize(Roles = "user, admin")]
        [HttpPost("User/{id}/Thumbnail")]
        public async Task<IActionResult> SetThumbnailUser([FromRoute] int id, IFormFile file)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");
            if (authorizationResult.Succeeded)
            {
                var ms = new MemoryStream();
                file.CopyTo(ms);
                await _usersService.SetThumbnailUser(id, ms.ToArray());
                return Ok();
            }
            else
                return Forbid();
        }
        
        [Authorize(Roles = "user, admin")]
        [HttpGet("User/{id}/Thumbnail")]
        public async Task<IActionResult> GetThumbnailUser([FromRoute] int id)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");
            if (authorizationResult.Succeeded)
            {
            return File(await _usersService.GetThumbnailUser(id), "image/jpeg");
            }
            else
                return Forbid();
        }
    }
}