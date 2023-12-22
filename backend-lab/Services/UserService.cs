using backend_lab.Models;

namespace backend_lab.Services;

public class UserService
{
    private List<User> _users = new()
    {
        new User() { Id = Guid.NewGuid(), Name = "Mila"},
        new User { Id = Guid.NewGuid(), Name = "Vika" },
        new User { Id = Guid.NewGuid(), Name = "Svitlanka" },
        new User { Id = Guid.NewGuid(), Name = "Vita" },
        new User { Id = Guid.NewGuid(), Name = "Alina" }
    };
    
    public IEnumerable<User> GetUsers()
    {
        return _users;
    } 

    public void AddUser(User user)
    { 
        if (GetUserById(user.Id) is not null)
            return;
        _users.Add(user);
    }

    public User GetUserById(Guid id) => 
        _users.FirstOrDefault(u => u.Id == id);

    public bool DeleteUserById(Guid id)
    {
        return _users.Remove(GetUserById(id));
    }
}