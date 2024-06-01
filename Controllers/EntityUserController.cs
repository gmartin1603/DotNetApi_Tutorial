using AutoMapper;
using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EntityUserController : ControllerBase
  {
    DataContextEf _entity;
    IMapper _mapper;

    public EntityUserController(IConfiguration config)
    {
      _entity = new DataContextEf(config);
      _mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<User, UserDto>().ReverseMap();
      }).CreateMapper();
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
      IEnumerable<User> users = _entity.User.ToList();
      return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
      // User user = _entity.User.FirstOrDefault(u => u.UserId == userId);
      User? user = _entity.User.Where(u => u.UserId == userId).FirstOrDefault();
      
      if (user == null)
      {
        throw new Exception("User not found");
      }
      return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
      User? userEntity = _entity.User.Where(u => u.UserId == user.UserId).FirstOrDefault();
      if (userEntity == null)
      {
        throw new Exception("User not found");
      }

      userEntity.FirstName = user.FirstName;
      userEntity.LastName = user.LastName;
      userEntity.Email = user.Email;
      userEntity.Gender = user.Gender;
      userEntity.Active = user.Active;
      
      if (_entity.SaveChanges() > 0){
        return Ok();
      }

      throw new Exception("Failed to update user");
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser(UserDto user)
    {
      // User userEntity = new User
      // {
      //   FirstName = user.FirstName,
      //   LastName = user.LastName,
      //   Email = user.Email,
      //   Gender = user.Gender,
      //   Active = user.Active
      // };
      User userEntity = _mapper.Map<User>(user);

      _entity.User.Add(userEntity);
      
      if (_entity.SaveChanges() > 0)
      {
        return Ok();
      }

      throw new Exception("Failed to create user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
      User? userEntity = _entity.User.Where(u => u.UserId == userId).FirstOrDefault();
      if (userEntity == null)
      {
        throw new Exception("User not found");
      }

      _entity.User.Remove(userEntity);
      
      if (_entity.SaveChanges() > 0)
      {
        return Ok();
      }

      throw new Exception("Failed to delete user");
    }

    
  }
}