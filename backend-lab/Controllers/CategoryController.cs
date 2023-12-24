using backend_lab.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace backend_lab.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // POST /category
    [HttpPost("category")]
    public async Task<IActionResult> PostCategory([FromBody] string categoryName)
    {
        try
        {
            var category = new Category { Name = categoryName, Id = Guid.NewGuid() };
            await _categoryService.AddCategory(category);
            return Ok(category.Id);
            
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // GET /category/<category_id>
    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        try
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category != null)
                return Ok(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
        return NotFound("Category not found.");

    }

    // DELETE /category/<category_id>
    [HttpDelete("category/{id}")]
    public async Task<IActionResult> DeleteCategoryById(Guid id)
    {
        try
        {
            await _categoryService.DeleteCategoryById(id);
            return Ok("Category deleted successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}