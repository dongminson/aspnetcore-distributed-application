using user.Entities;
using user.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace user.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMessageService _messageService;
        public UsersController(IUserRepository userRepository, ITokenService tokenService, IMessageService messageService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        { 
            var users = await _userRepository.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            return user;
        }

        [Authorize]
        [HttpPost("geolocation/hide/{userId}")]
        public ActionResult ToggleVisibilityOff(int userId)
        {
            var userIdFromToken = _tokenService.GetUserIdFromToken(HttpContext.Request);

            if (userId != userIdFromToken)
            {
                return Unauthorized("User ID does not match the token");
            }

            _messageService.Enqueue(userId, false);

            return Ok("Visibility toggled on for user " + userId);
        }
        
        [Authorize]
        [HttpPost("geolocation/show/{userId}")]
        public ActionResult ToggleVisibilityOn(int userId)
        {
            var userIdFromToken = _tokenService.GetUserIdFromToken(HttpContext.Request);

            if (userId != userIdFromToken)
            {
                return Unauthorized("User ID does not match the token");
            }

            _messageService.Enqueue(userId, true);
            
            return Ok("Visibility toggled on for user " + userId);
        }



    }
}