using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Services.Providers
{
    public class Users : List<User>
    {
        public static Users Load()
        {
            Users users = new Users
            {
                new User()
                {
                    UserName = "karina",
                    Roles = new List<string>() { "user", "admin" }
                },
                new User()
                {
                    UserName = "johny",
                    Roles = new List<string>() { "user" }
                },
                new User()
                {
                    UserName = "tom",
                    Roles = new List<string>() { "user" }
                }
            };

            return users;
        }

        public User FindByName(string userName)
        {
            return this.Find(u => u.UserName == userName);
        }
    }

    public class User
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    }
}
