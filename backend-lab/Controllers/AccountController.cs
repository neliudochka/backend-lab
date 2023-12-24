
using Models;
using backend_lab.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_lab.Controllers;

[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("account")]
    public async Task<IActionResult> AddAccount([FromBody]Guid userId)
    {
        try
        {
            await _accountService.AddAccount(userId);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("account/{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        try
        {
            return Ok(await _accountService.GetAccount(userId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    
    
    [HttpPost("account/topUp")]
    public async Task<IActionResult> TopUp([FromBody]AccountTopUpDownRequest req)
    {
        try
        {
            await _accountService.TopUpAccount(req.UserId, req.Money);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("account/topDown")]
    public async Task<IActionResult> TopDown([FromBody]AccountTopUpDownRequest req)
    {
        try
        {
            await _accountService.TopDownAccount(req.UserId, req.Money);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}