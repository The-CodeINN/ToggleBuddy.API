using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUser _user;
        private readonly ApiResponse<object> _apiResponse = new ApiResponse<object>();

        public UserController(IMapper mapper, IUser user)
        {
            _mapper = mapper;
            _user = user;
        }

        // GET: api/User/me
        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var currentUser = await _user.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                _apiResponse.Message = "User not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var userResponseDto = _mapper.Map<UserDto>(currentUser);

            _apiResponse.Result = userResponseDto;
            _apiResponse.Message = "User found";

            return Ok(_apiResponse);
        }
    }
};
