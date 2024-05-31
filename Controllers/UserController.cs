using DotNetApi.Data;
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

    [HttpGet("GetUsers/{name}")]
    // public IActionResult Test()
    public string[] GetUsers(string name)
    {
      string[] res = ["Hello", "World", name];
      return res;
    }

  }
}