using backend_lab.Models;
using backend_lab.Services;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<Category> PostCategory([FromBody] string categoryName)
    {
        var category = new Category { Name = categoryName, Id = Guid.NewGuid() };
        _categoryService.AddCategory(category);
        return Ok(category.Id);
    }

    // GET /category/<category_id>
    [HttpGet("category/{id}")]
    public ActionResult<Category> GetCategoryById(Guid id)
    {
        var category = _categoryService.GetCategoryById(id);
        if (category != null) return Ok(category);
        return NotFound("No category with such id");
    }

    // DELETE /category/<category_id>
    [HttpDelete("category/{id}")]
    public ActionResult DeleteCategoryById(Guid id)
    {
        var category = _categoryService.DeleteCategoryById(id);
        if (category)
            return Ok("Category deleted successfully.");
        return NotFound("Category not found.");
    }
}