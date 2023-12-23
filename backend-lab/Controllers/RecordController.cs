using Models;
using backend_lab.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_lab.Controllers;

[ApiController]
public class RecordsController : ControllerBase
{
    private readonly RecordService _recordService;
    
    public RecordsController(RecordService recordService)
    {
        _recordService = recordService;
    }
    
    //get by id
    [HttpGet("record/{id}")]
    public async Task<IActionResult> GetRecordById(Guid id)
    {
        var record = await _recordService.GetRecordById(id);
        if (record != null) 
            return Ok(record);
        return NotFound("No record with such id");
    }

    //delete 
    [HttpDelete("record/{id}")]
    public async Task<IActionResult> DeleteRecordById(Guid id)
    {
        try
        {
            await _recordService.DeleteRecordById(id);
            return Ok("Record deleted successfully.");
        }
        catch (NullReferenceException exception)
        {
            return NotFound("Record not found.");
        }
    }

    //post
    [HttpPost("record")]
    public async Task<IActionResult> PostRecord([FromBody]CreateRecordRequest request)
    {
        var id = Guid.NewGuid();
        await _recordService.AddRecord(new Record(
            id,
            request.UserId,
            request.CategoryId,
            request.MoneySpent));
        return Ok(id);
    }
    
    
    //get by user/cat
    [HttpGet("record")]
    public async Task<IActionResult> GetByUserOrAndCategory([FromQuery] RecordRequest request)
    {
        if (request.categoryId is null && request.userId is null)
            return BadRequest("No such category and user in request!!1");
        var record = await _recordService.GetRecords(request);
        return Ok(record);
    }
    
    
}