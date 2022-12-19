using WebAPI.Entities;

namespace WebAPI.Services;
public interface IUsersService {
    Task SaveChangesAsync();

    Task CreateUser(Users users);

    Task<Users?> GetUser(int userId);
    
    Task<IEnumerable<Users>> GetUsers();

    Task Deleteuser(Users user);
}