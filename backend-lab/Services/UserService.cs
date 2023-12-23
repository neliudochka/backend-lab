using Models;

namespace backend_lab.Services;

public class UserService
{
    private readonly RepoPull _repoPull;

    public UserService(RepoPull repoPull)
    {
        _repoPull = repoPull;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var users = await _repoPull.UserRepo.GetAllAsync();
        return users;
    } 

    public async Task AddUser(User user)
    {
        await _repoPull.UserRepo.AddAsync(user);
    }

    public Task<User> GetUserById(Guid id)
    {
        return _repoPull.UserRepo.GetByIdAsync(id);
    }

    public Task DeleteUserById(Guid id)
    {
        return _repoPull.UserRepo.DeleteByIdAsync(id);
    }
}