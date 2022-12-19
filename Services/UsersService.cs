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

    public async Task<Users?> GetUser(int userId)
    {
        return await context.Users.Where(c => c.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Users>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
       await context.SaveChangesAsync();
    }
}