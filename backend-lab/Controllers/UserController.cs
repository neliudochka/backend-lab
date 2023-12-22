using Microsoft.AspNetCore.Mvc;
using backend_lab.Models;
using backend_lab.Services;

namespace backend_lab.Controllers;


[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userServiceService)
    {
        _userService = userServiceService;
    }
    
    //GET /users
    [HttpGet("users")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_userService.GetUsers());
    }

    //POST /user
    [HttpPost("user")]
    public ActionResult<User> PostUser([FromBody] string userName)
    {
        var user = new User {Name = userName, Id = Guid.NewGuid()};
        _userService.AddUser(user);
        return Ok(user.Id);
    }
    
    //GET /user/<user_id>
    [HttpGet("user/{id}")]
    public ActionResult<User> GetUserById(Guid id)
    {
        var user = _userService.GetUserById(id);
        if (user != null)
        {
            return Ok(user);
        }
        return NotFound("No user with such id");
    }
    
    // DELETE /user/<user_id>\
    [HttpDelete("user/{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        var userToRemove = _userService.DeleteUserById(id);
        if (userToRemove)
        {
            return Ok("User deleted successfully.");
        }
        else
        {
            return NotFound("User not found.");
        }
        
    }
}
