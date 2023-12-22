using backend_lab.Models;

namespace backend_lab.Services;

public class CategoryService {
    private readonly List<Category> _categories = new()
    {
        new Category { Id = Guid.NewGuid(), Name = "Health" },
        new Category { Id = Guid.NewGuid(), Name = "Entertainment" },
    };
    
    public void AddCategory(Category category)
    {
        if (GetCategoryById(category.Id) is not null)
            return;
        _categories.Add(category);
    }

    public Category GetCategoryById(Guid id)
    {
        return _categories.FirstOrDefault(c => c.Id == id);
    }
    
    public bool DeleteCategoryById(Guid id)
    {
        var category = GetCategoryById(id);
        if (category != null)
        {
            _categories.Remove(category);
            return true;
        }
        return false;
    }
}

