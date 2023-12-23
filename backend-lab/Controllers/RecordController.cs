using backend_lab.Models;
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
    public IActionResult GetRecordById(Guid id)
    {
        var record = _recordService.GetRecordById(id);
        if (record != null) return Ok(record);
        return NotFound("No category with such id");
    }

    //delete 
    [HttpDelete("record/{id}")]
    public IActionResult DeleteRecordById(Guid id)
    {
        var record = _recordService.DeleteRecordById(id);
        if (record)
            return Ok("Category deleted successfully.");
        return NotFound("Category not found.");
    }

    //post
    [HttpPost("record")]
    public IActionResult PostRecord([FromBody]CreateRecordRequest request)
    {
        var id = Guid.NewGuid();
        _recordService.AddRecord(new Record(
            id,
            request.UserId,
            request.CategoryId,
            DateTime.Now, 
            request.MoneySpent));
        return Ok(id);
    }
    
    
    //get by user/cat
    [HttpGet("record")]
    public IActionResult GetByUserOrAndCategory([FromQuery] RecordRequest request)
    {
        if (request.categoryId is null && request.userId is null)
            return BadRequest("No such category and user in request!!1");
        var record = _recordService.GetRecords(request);
        return Ok(record);
    }
    
    
}