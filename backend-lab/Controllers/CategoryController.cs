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
        var category = new Category { Name = categoryName, Id = Guid.NewGuid() };
        await _categoryService.AddCategory(category);
        return Ok(category.Id);
    }

    // GET /category/<category_id>
    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if (category != null)
        {
            return Ok(category);
        }
        return NotFound("No category with such id");

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
        catch (NullReferenceException exception)
        {
            return NotFound("Category not found.");
        }
    }
}