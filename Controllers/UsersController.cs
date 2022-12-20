using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly IUsersService userRepo;

    private readonly IMapper mapper;
    public UsersController(
        ILogger<UsersController> logger,
        IUsersService userRepo,
        IMapper mapper
    )
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userRepo = userRepo ?? throw new ArgumentNullException(nameof(logger));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsersDto>>> getUsers(
        string? name, 
        string? search,
        int pageSize = 10,
        int pageNumber = 1
    ) {
        var (result, paginationMeta) = await userRepo.GetUsers(name, search, pageSize, pageNumber);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMeta));

        return Ok(mapper.Map<IEnumerable<UsersDto>>(result));
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public async Task<ActionResult<UsersDto>> getUser(
        Guid userId
    ) {
        var user = await userRepo.GetUser(userId);

        if(user == null){
            return NotFound();
        }


        return Ok(mapper.Map<UsersDto>(user));
    }
    
    [HttpPost]
    public async Task<ActionResult<UsersDto>> createUser(
        CreateUserDto userDto
    ) {
        var userMapped = mapper.Map<Users>(userDto);
        
        await userRepo.CreateUser(userMapped);

        var result = mapper.Map<UsersDto>(userMapped);

        return CreatedAtRoute(
            "GetUser",
            new {
                userId =result.Id
            },
            result
        );
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<UsersDto>> updateUser(
        Guid userId,
        UpdateUserDto userDto
    ) {
        var user = await userRepo.GetUser(userId);

        if(user == null) {
            return NotFound();
        }

        var result = mapper.Map(userDto, user);

        await userRepo.SaveChangesAsync();

        return Ok(mapper.Map<UpdateUserDto>(result));
    }

    [HttpPatch("{userId}")]
    public async Task<ActionResult<UsersDto>> patchUser(
        Guid userId,
        JsonPatchDocument<UpdateUserDto> userDto
    ) {
       var user = await userRepo.GetUser(userId);
       
       if(user == null) {
        return NotFound();
       }

       var userToPatch = mapper.Map<UpdateUserDto>(user);
       
       userDto.ApplyTo(userToPatch, ModelState);

       if(!ModelState.IsValid){
            return BadRequest(ModelState);
       }

       if(!TryValidateModel(userToPatch)){
            return BadRequest(ModelState);
       }

       mapper.Map(userToPatch, user);
       await userRepo.SaveChangesAsync();

       return Ok(userToPatch);
    }
    
    [HttpDelete("{userId}")]
    public async Task<ActionResult<UsersDto>> deleteUser(
        Guid userId
    ) {
        var user = await userRepo.GetUser(userId);
       
       if(user == null) {
        return NotFound();
       }

        await userRepo.Deleteuser(user);

       return Ok((new {
        Deleted = new {
            User = mapper.Map<UsersDto>(user)
        }
       }));
    }
}