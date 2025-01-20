
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    public UserCompleteController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    // get-endpoint
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }
    // get-endpoint
    [HttpGet("GetUsers/{userId}/{isActive}")]
    // public IActionResult Test() //API controller
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
    {
        string sql = @"EXEC TutorialAppSchema.spUsers_Get";
        string parameters = "";

        if (userId != 0)
        {
            parameters += ", @UserId = " + userId.ToString();
        }
        if (isActive)
        {
            parameters += ", @Active = " + isActive.ToString();
        }

        sql += parameters.Substring(1); //, parameters.Length);

        Console.WriteLine(sql);

        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);

        return users;
    }

    [HttpPut("UpsertUser")]
    public IActionResult Upsert(UserComplete user)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Upsert
            @FirstName = '" + user.FirstName +
            "', @LastName = '" + user.FirstName +
            "', @Email = '" + user.Email +
            "', @Gender = '" + user.Gender +
            "', @Active = '" + user.Active +
            "', @JobTitle = '" + user.JobTitle +
            "', @Department = '" + user.Department +
            "', @Salary = '" + user.Salary +
            "', @UserId = " + user.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // is the builtin function from ControllerBase
        }

        throw new Exception("Failed to Udate User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult Deleteuser(int userId)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Delete
                @UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }

}
