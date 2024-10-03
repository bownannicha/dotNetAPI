
using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    // create connection for this interface in Program.cs before building()
    public interface IUserRepository
    {
        // connect this Interface by call below instance of those class method
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToRemove);
        public User GetSingleUser(int userId);
        public UserSalary GetSingleUserSalary(int userId);
        public UserJobInfo GetSingleUserJobInfo(int userId);


    }
}