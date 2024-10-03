
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
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
    [HttpGet("GetUsers")]


    // public IActionResult Test() //API controller
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT  [UserId]
                , [FirstName]
                , [LastName]
                , [Email]
                , [Gender]
                , [Active]
            FROM  TutorialAppSchema.Users;";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);

        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
            SELECT  [UserId]
                , [FirstName]
                , [LastName]
                , [Email]
                , [Gender]
                , [Active]
            FROM  TutorialAppSchema.Users
                WHERE UserId = " + userId.ToString();
        User user = _dapper.LoadDataSingle<User>(sql);

        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName +
            "',[LastName] = '" + user.FirstName +
            "',[Email] = '" + user.Email +
            "',[Gender] = '" + user.Gender +
            "',[Active] = '" + user.Active +
            "' WHERE UserId = " + user.UserId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // is the builtin function from ControllerBase
        }

        throw new Exception("Failed to Udate User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user)
    {
        string sql = @"INSERT INTO TutorialAppSchema.Users (
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            ) VALUES (" +
            "'" + user.FirstName +
            "','" + user.FirstName +
            "','" + user.Email +
            "','" + user.Gender +
            "','" + user.Active +
            "')";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult Deleteuser(int userId)
    {
        string sql = @"
            DELETE FROM  TutorialAppSchema.Users
                WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }

    [HttpGet("GetSalaries")]
    public IEnumerable<UserSalary> GetSalaries()
    {
        string sql = @"
            SELECT  [UserId]
                , [Salary]
                , [AvgSalary]
            FROM  TutorialAppSchema.UserSalary;";
        IEnumerable<UserSalary> users = _dapper.LoadData<UserSalary>(sql);

        return users;
    }

    [HttpGet("GetSingleSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql = @"
            SELECT  [UserId]
                , [Salary]
                , [AvgSalary]
            FROM  TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();
        UserSalary user = _dapper.LoadDataSingle<UserSalary>(sql);

        return user;
    }

    [HttpPut("UserSalary")]
    public IActionResult UpdateUserSalary(UserSalary userSalary)
    {
        string sql = @"
        UPDATE TutorialAppSchema.UserSalary
            SET Salary = " + userSalary.Salary.ToString(System.Globalization.CultureInfo.InvariantCulture) +
            " WHERE UserId = " + userSalary.UserId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userSalary); // is the builtin function from ControllerBase
        }

        throw new Exception("Failed to Udate UserSalary");
    }


}


