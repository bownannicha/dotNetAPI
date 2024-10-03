
using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework; // to delete after done all UserRepository and IUserRepository
    IUserRepository _userRepository;

    // Mapper
    IMapper _mapper;
    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
        _entityFramework = new DataContextEF(config);

        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserDto, User>(); // we want to map from UserDto with User
        }));
    }
    // get-endpoint
    [HttpGet("GetUsers")]


    // public IActionResult Test() //API controller
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();

        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        /*** With userRepository ****/
        return _userRepository.GetSingleUser(userId);

        /* --Without UserRepository ---*/
        // User? user = _entityFramework.Users
        //     .Where(u => u.UserId == userId)
        //     .FirstOrDefault<User>();

        // if (user != null)
        // {
        //     return user;
        // }
        // throw new Exception("Failed to Get User");

    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        //  Pulling a target user from the Db
        User? userDb = _entityFramework.Users
           .Where(u => u.UserId == user.UserId)
           .FirstOrDefault<User>();

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");
        }
        throw new Exception("Failed to Get User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user)
    {
        // Use auto Mapper
        User userDb = _mapper.Map<User>(user);

        //  Create a new user or object user
        /* ** without auto Mapper
        User userDb = new User();
        userDb.Active = user.Active;
        userDb.FirstName = user.FirstName;
        userDb.LastName = user.LastName;
        userDb.Email = user.Email;
        userDb.Gender = user.Gender;
        */


        _userRepository.AddEntity<User>(userDb);

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult Deleteuser(int userId)
    {
        User? userDb = _entityFramework.Users
           .Where(u => u.UserId == userId)
           .FirstOrDefault<User>();

        if (userDb != null)
        {
            // _entityFramework.Users.Remove(userDb);
            _userRepository.RemoveEntity<User>(userDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");

        }
        throw new Exception("Failed to Get User");
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        return _userRepository.GetSingleUserSalary(userId);

    }

    [HttpGet("GetUserSalaries")]
    public IEnumerable<UserSalary> GetUserSalaries()
    {
        IEnumerable<UserSalary> users = _entityFramework.UserSalary.ToList<UserSalary>();

        return users;
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalaryEF(UserSalary userForInsert)
    {
        _userRepository.AddEntity<UserSalary>(userForInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add UserSalary");
    }


    [HttpPut("UpdateUserSalary")]
    public IActionResult UpdateUserSalary(UserSalary user)
    {
        UserSalary? userDb = _entityFramework.UserSalary
           .Where(u => u.UserId == user.UserId)
           .FirstOrDefault<UserSalary>();

        if (userDb != null)
        {
            userDb.Salary = user.Salary;
            userDb.AvgSalary = user.AvgSalary;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update UserSalary");
        }
        throw new Exception("Failed to Get UserSalary");
    }

    [HttpGet("GetSingleUserJobInfo")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        return _userRepository.GetSingleUserJobInfo(userId);
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfoEF(UserJobInfo userForInsert)
    {
        _userRepository.AddEntity<UserJobInfo>(userForInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Save UserJobInfo");
    }

    [HttpPut("UserJobInfo")]
    public IActionResult UpdateUserJobInfoEF(UserJobInfo userJobInfo)
    {
        UserJobInfo? userToUpdate = _userRepository.GetSingleUserJobInfo(userJobInfo.UserId);
        if (userToUpdate != null)
        {
            _mapper.Map(userToUpdate, userJobInfo);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
        }
        throw new Exception("Failed on Update UserJobinfo");
    }

}


