namespace WebAPI.Models;

public class UsersDto
{
    public Guid Id { get; set; }

    public string Name {get; set;} = string.Empty;

    public int Age {get; set;}
}