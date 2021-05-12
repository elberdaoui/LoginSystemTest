using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using LoginSystemTest.DataAccess;

namespace LoginSystemTest.BAL
{
    public class UserLogic
    {
        private readonly loginTestEntities entities;

        public UserLogic()
        {
            entities = new loginTestEntities();
        }

        public List<User> GetUsers()
        {
            //var users = entities.Users.Select(u => new { u.FirstName, u.LastName, u.UserName }).ToList();
            //var list = new List<object>();
            //foreach (var user in users) list.Add(user);
            ////return list.ToList();
            ////return entities.Users.ToList();
            var usr = (from u in entities.Users
                select u).ToList();
            var result = from u in usr
                select new User()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName
                };
            return result.ToList();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            entities.Users.Add(user);
            await entities.SaveChangesAsync();
            return user;
        }

        public List<User> Login(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return null;
                }
                var result = entities.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
                if (result.UserName != userName || result.Password != password)
                {
                    // return new CustomError(statusCode: 404, Uri("UserService.asmx"));
                    return null;
                }
                return GetUsers();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
                //e.Message("something wrong");
            }
            
            
        }
    }
}