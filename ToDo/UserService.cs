using Dapper;
using Microsoft.Data.SqlClient;
using ToDo.Models.User;

namespace ToDo
{
  public class UserService
  {
    public async Task<User> GetAsync(int userID)
    {
      DynamicParameters p = new DynamicParameters();
      p.Add("@UserID", userID);

      IEnumerable<User> result;

      using (var connection = new SqlConnection(ConfigurationSingleton.Instance.ConnectionString))
      {
        result = await connection.QueryAsync<User>("""
          SELECT * 
          FROM [User] 
          WHERE [UserID] = @UserID
          """, p);
      }

      return result.FirstOrDefault()!;
    }

    public async Task<User> GetAsync(string email)
    {
      DynamicParameters p = new DynamicParameters();
      p.Add("@Email", email);

      IEnumerable<User> result;

      using (var connection = new SqlConnection(ConfigurationSingleton.Instance.ConnectionString))
      {
        result = await connection.QueryAsync<User>("""
          SELECT * 
          FROM [User] 
          WHERE [Email] = @Email
          """, p);
      }

      return result.FirstOrDefault()!;
    }
  }
}