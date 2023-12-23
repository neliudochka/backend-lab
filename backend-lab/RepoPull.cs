using DataLayer;
using Models;

namespace backend_lab;

public class RepoPull
{
    private readonly DataContext _context;

    public Repo<User> UserRepo { get; }
    public Repo<Record> RecordRepo { get; }
    public Repo<Category> CategoryRepo { get; }

    public RepoPull(DataContext context)
    {
        _context = context;
        UserRepo = new Repo<User>(context);
        CategoryRepo = new Repo<Category>(context);
        RecordRepo = new Repo<Record>(context);
    }

    public Task SaveAsync() => _context.SaveChangesAsync();
}