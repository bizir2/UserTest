using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using UsersTest.Auth;
using UsersTest.Entites;
using UsersTest.Interfaces;
using UserTestLibrary.Models.CreateUser;
using UserTestLibrary.Models.RemoveUser;
using UserTestLibrary.Models.User;

namespace UsersTest.Controllers;

[Authorize(nameof(BasicAuthenticationHandler.BasicAuthentication))]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    readonly private IUserDbOrCache _userDbOrCache;
    public AuthController(IMapper mapper, IUserDbOrCache userDbOrCache)
    {
        _mapper = mapper;
        _userDbOrCache = userDbOrCache;
    }

    [HttpPost("CreateUser")]
    [Produces("application/xml")]
    public async Task<IActionResult> CreateUser([FromBody]CreateUser user)
    {
        var userDb = await _userDbOrCache.ById(user.User.Id);
        if (userDb != null)
        {
            return StatusCode(400, ResponseError.Create($"User with id {userDb.ID} already exist"));
        }

        var u = _mapper.Map<UserDocument>(user.User);
        
        try
        {
            await _userDbOrCache.Create(u);
        }
        catch (MySqlException e)
        {
            if (e.ErrorCode == MySqlErrorCode.DuplicateKey)
            {
                return StatusCode(400, ResponseError.Create($"User with id {userDb.ID} already exist"));
            }
            throw;
        }
        
        return Ok(ResponseSuccess.Create(user.User));
    }

    [HttpPost("RemoveUser")]
    [Produces("application/json")]
    public async Task<IActionResult> RemoveUser([FromBody] RemoveUser removeUser)
    {
        var userDb = await _userDbOrCache.ById(removeUser.RemoveUserId.Id);
        if (userDb == null)
        {
            return StatusCode(400, RemoveUserResponse.ErrorResponse("User not found"));
        }
        
        await _userDbOrCache.Remove(userDb);
        
        var u = _mapper.Map<UserDto>(userDb);
        return Ok(RemoveUserResponse.SuccessResponse("User was removed", u));
    }

    [HttpPost("SetStatus")]
    [Produces("application/json")]
    public async Task<IActionResult> SetStatus([FromForm]UserDto user)
    {
        var userDb = await _userDbOrCache.ById(user.Id);
        if (userDb == null) return StatusCode(400, null);
        
        userDb.Status = user.Status;
        _userDbOrCache.SetStatus(userDb);
        
        var u = _mapper.Map<UserDto>(userDb);
        return Ok(u);
    }
}