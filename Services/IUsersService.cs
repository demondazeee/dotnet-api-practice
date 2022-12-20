using WebAPI.Entities;

namespace WebAPI.Services;
public interface IUsersService {
    Task SaveChangesAsync();

    Task CreateUser(Users users);

    Task<Users?> GetUser(Guid userId);
    
    Task<IEnumerable<Users>> GetUsers();
    Task<(IEnumerable<Users>, PaginationMetadata)> GetUsers(
        string? name,
        string? search,
        int pageSize,
        int pageNumber
    );

    Task Deleteuser(Users user);
}