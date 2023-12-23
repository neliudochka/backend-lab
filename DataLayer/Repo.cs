using Microsoft.EntityFrameworkCore;
using Models;

namespace DataLayer;

public class Repo<T> where T : BaseModel
{
    public readonly DataContext Context;

    public Repo(DataContext context)
    {
        Context = context;
    }
    
    public async Task<T> GetByIdAsync(Guid id)
        => await Context.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    

    public async Task<T> UpdateAsync(T entity)
    {
        var entityToUpdate = await Context.Set<T>().FirstAsync(x => x.Id.Equals(entity.Id));
        entityToUpdate = entity;
        await SaveAsync();
        return entityToUpdate;
    }

    public async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await SaveAsync();
    }
    

    public async Task<T> DeleteByIdAsync(Guid id)
    {
        var model = await GetByIdAsync(id);
        Context.Set<T>().Remove(model);
        await SaveAsync();
        return model;
    }

    public Task<List<T>> GetAllAsync()
        => Context.Set<T>().ToListAsync();
    
    public Task SaveAsync()
        => Context.SaveChangesAsync();
    
}