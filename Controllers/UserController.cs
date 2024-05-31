using DotNetApi.Data;
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
    public IEnumerable<User> GetSingleUser(int userId)
    {
      IEnumerable<User> user = _dapper.LoadData<User>($"SELECT * FROM TutorialAppSchema.Users WHERE UserId = {userId}");
      return user;
    }

  }
}