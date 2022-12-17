using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<UsersDto>> getUsers() {
        var users = UsersData.UserData;

        return Ok(users);
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public ActionResult<UsersDto> getUser(
        int userId
    ) {
        var user = UsersData.UserData.FirstOrDefault(user => user.Id == userId);

        if(user == null){
            return NotFound();
        }

        return Ok(user);
    }
    
    [HttpPost]
    public ActionResult<UsersDto> createUser(
        CreateUserDto userDto
    ) {
        
        var newUser = new UsersDto() {
            Id = ++UsersData.UsersCount,
            Name = userDto.Name,
            Age = userDto.Age
        };

        UsersData.UserData.Add(newUser);
        
        return CreatedAtRoute(
            "GetUser",
            new {
                userId=newUser.Id
            },
            newUser
        );
    }

    [HttpPut("{userId}")]
    public ActionResult<UsersDto> updateUser(
        int userId,
        UpdateUserDto userDto
    ) {
        var user = UsersData.UserData.FirstOrDefault(user => user.Id == userId);

        if(user == null){
            return NotFound();
        }

        user.Name = userDto.Name;
        user.Age = userDto.Age;



        return Ok(user);
    }

    [HttpPatch("{userId}")]
    public ActionResult<UsersDto> patchUser(
        int userId,
        JsonPatchDocument<UpdateUserDto> userDto
    ) {
        var user = UsersData.UserData.FirstOrDefault(user => user.Id == userId);

        if(user == null){
            return NotFound();
        }

        var updatedUser = new UpdateUserDto() {
            Name = user.Name,
            Age = user.Age
        };

        userDto.ApplyTo(updatedUser, ModelState);
        
        if(!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        if(!TryValidateModel(userDto)) {
            return BadRequest(ModelState);
        }

        user.Name = updatedUser.Name;
        user.Age = updatedUser.Age;

        return Ok(updatedUser);
    }
    
    [HttpDelete("{userId}")]
    public ActionResult<UsersDto> deleteUser(
        int userId
    ) {
        var user = UsersData.UserData.FirstOrDefault(user => user.Id == userId);
        if(user == null){
            return NotFound();
        }

        UsersData.UserData.Remove(user);

        return Ok(user);
    }
}