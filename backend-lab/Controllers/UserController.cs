using Microsoft.AspNetCore.Mvc;
using Models;
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
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetUsers();
            return Ok(users);

        } catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    //POST /user
    [HttpPost("user")]
    public async Task<IActionResult> PostUser([FromBody] string userName)
    {
        try
        {
            var user = new User { Name = userName, Id = Guid.NewGuid() };
            await _userService.AddUser(user);
            return Ok(user.Id);
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    //GET /user/<user_id>
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
                return Ok(user);
            return NotFound("No user with such id");
        }  catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    // DELETE /user/<user_id>\
    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUserById(Guid id)
    {
        try
        {
            await _userService.DeleteUserById(id);
            return Ok("User deleted successfully.");
        }
        catch (NullReferenceException exception)
        {
            return NotFound("User not found.");
        }
    }
}
