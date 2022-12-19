using AutoMapper;
using WebAPI.Entities;
using WebAPI.Models;

class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<UsersDto, Users>();
        CreateMap<CreateUserDto, Users>();
        CreateMap<UpdateUserDto, Users>();
        CreateMap<Users, CreateUserDto>();
        CreateMap<Users, UpdateUserDto>();
        CreateMap<Users, UsersDto>();
    }
}