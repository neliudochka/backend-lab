using Models;

namespace backend_lab.Services;

public class CategoryService {
    private readonly RepoPull _repoPull;
    public CategoryService(RepoPull repoPull)
    {
        _repoPull = repoPull;
    }

    public async Task AddCategory(Category category)
    {
        await _repoPull.CategoryRepo.AddAsync(category);
    }

    public Task<Category> GetCategoryById(Guid id)
    {
        return _repoPull.CategoryRepo.GetByIdAsync(id);
    }
    
    public Task DeleteCategoryById(Guid id)
    {
        return _repoPull.CategoryRepo.DeleteByIdAsync(id);
    }
}

