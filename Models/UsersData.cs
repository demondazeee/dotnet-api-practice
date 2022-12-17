namespace WebAPI.Models;

public class UsersData
{
    public static ICollection<UsersDto> UserData {get; set;} = new List<UsersDto>() {
        new UsersDto() {
            Id = 1,
            Name = "testuser1",
            Age = 12
        },
        new UsersDto() {
            Id = 2,
            Name = "testuser1",
            Age = 12
        },
    };

    public static int UsersCount {get; set;} = UsersData.UserData.Count;
}