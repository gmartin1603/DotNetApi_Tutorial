using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotNetApi.Data
{
  public class DataContextDapper
  {
    private readonly IConfiguration _config;
    public DataContextDapper(IConfiguration config)
    {
      _config = config;
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {
      // string connectionString = _config.GetConnectionString("DefaultConnection");
      using IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return connection.Query<T>(sql);
    }

    public T LoadSingleData<T>(string sql)
    {
      IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return connection.QuerySingle<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
      IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return connection.Execute(sql) > 0;
    }

    public int ExecuteSqlWithRowCount(string sql)
    {
      IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return connection.Execute(sql);
    }
  }
}