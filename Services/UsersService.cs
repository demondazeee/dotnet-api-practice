using WebAPI.Entities;
using WebAPI.DBContext;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services;

public class UsersService : IUsersService
{
    private readonly Db context;
    public UsersService(Db context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task CreateUser(Users users)
    {
        await context.Users.AddAsync(users);

        await this.SaveChangesAsync();
    }

    public async Task Deleteuser(Users user)
    {
        context.Users.Remove(user);
        await this.SaveChangesAsync();
    }

    public async Task<Users?> GetUser(Guid userId)
    {
        return await context.Users.Where(c => c.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Users>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<(IEnumerable<Users>, PaginationMetadata)> GetUsers(string? name, string? search, int pageSize, int pageNumber)
    {
        var collection = context.Users as IQueryable<Users>;
        if(!string.IsNullOrWhiteSpace(name)){
            name = name.Trim();
            collection.Where(u => u.Name == name);
        }

        if(!string.IsNullOrWhiteSpace(search)){
            search = search.Trim();
            collection.Where(u => u.Name.Contains(search));
        }
        var totalCount = await collection.CountAsync();

        var paginationMeta = new PaginationMetadata(totalCount, pageSize, pageNumber);

        var results = await collection
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (results, paginationMeta);
    }

    public async Task SaveChangesAsync()
    {
       await context.SaveChangesAsync();
    }
}