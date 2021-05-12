using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using LoginSystemTest.BAL;
using LoginSystemTest.DataAccess;

namespace LoginSystemTest
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        private readonly UserLogic userLogic;

        public UserService()
        {
            userLogic = new UserLogic();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<User> GetAll()
        {
            return userLogic.GetUsers();
        }

        [WebMethod]
        public async Task<User> CreateUser(int id, string firstName, string lastName, string userName, string password)
        {
            var user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password
            };
            await userLogic.CreateUserAsync(user);
            return user;
        }

        [WebMethod]
        public List<User> Login(string username, string password)
        {
            
            var use =  userLogic.Login(username, password);
            return use;
        }
    }
}
