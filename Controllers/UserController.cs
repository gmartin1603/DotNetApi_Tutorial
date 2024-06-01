using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
      _dapper = new DataContextDapper(config);
    }

    [HttpGet("testConnection")]
    public DateTime TestConnection()
    {
      return _dapper.LoadSingleData<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
      IEnumerable<User> users = _dapper.LoadData<User>("SELECT * FROM TutorialAppSchema.Users");
      return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
      User user = _dapper.LoadSingleData<User>($"SELECT * FROM TutorialAppSchema.Users WHERE UserId = {userId}");
      return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
      string sql = $@"
        UPDATE TutorialAppSchema.Users
        SET [FirstName] = '{user.FirstName}',
            [LastName] = '{user.LastName}',
            [Email] = '{user.Email}',
            [Gender] = '{user.Gender}',
            [Active] = '{user.Active}'
        WHERE [UserId] = '{user.UserId}'";
      if (_dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      // return BadRequest();
      throw new Exception("Failed to update user");
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser(UserDto user)
    {
      string sql = $@"
        INSERT INTO TutorialAppSchema.Users (
          [FirstName], 
          [LastName], 
          [Email],
          [Gender],
          [Active]
        ) VALUES (
          '{user.FirstName}',
          '{user.LastName}',
          '{user.Email}',
          '{user.Gender}',
          '{user.Active}'
        )";
      if (_dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      // return BadRequest();
      throw new Exception("Failed to create user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
      string sql = $"DELETE FROM TutorialAppSchema.Users WHERE UserId = {userId}";
      if (_dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      // return BadRequest();
      throw new Exception("Failed to delete user");
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
      UserSalary userSalary = _dapper.LoadSingleData<UserSalary>($"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}");
      return userSalary;
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
      UserJobInfo userJobInfo = _dapper.LoadSingleData<UserJobInfo>($"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}");
      return userJobInfo;
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
      string sql = $@"
        UPDATE TutorialAppSchema.UserSalary
        SET [Salary] = '{userSalary.Salary}'
        WHERE [UserId] = '{userSalary.UserId}'";
      if (_dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      // return BadRequest();
      throw new Exception("Failed to update user salary");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
      string sql = $@"
        UPDATE TutorialAppSchema.UserJobInfo
        SET [JobTitle] = '{userJobInfo.JobTitle}',
            [Department] = '{userJobInfo.Department}'
        WHERE [UserId] = '{userJobInfo.UserId}'";
      if (_dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      // return BadRequest();
      throw new Exception("Failed to update user job info");
    }
    
  }
}