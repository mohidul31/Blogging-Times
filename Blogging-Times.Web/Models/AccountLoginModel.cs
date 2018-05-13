using Blogging_Times.Data;
using Blogging_Times.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Blogging_Times.Web.Models
{
    public class AccountLoginModel
    {
        private  PostDbContext db = new PostDbContext();

        public AccountLoginModel()
        {
            int oldAdmin = db.Users.Where(x => x.Username.ToLower().Contains("admin")).Count();
            if (oldAdmin == 0)
            {
                //Create Admin Username
                Users user = new Users();
                user.Username = "admin";
                user.Password = Crypto.SHA256("123456");
                user.UserRoleEnum = UserRoleEnum.Admin;
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public bool CheckLogin(string username, string password, bool rememberMe)
        {
            string hash_password = Crypto.SHA256(password);
            int res = db.Users.Where(w => w.Username.ToLower().Contains(username.ToLower()) 
                                    && w.Password == hash_password).Count();
            if (res == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Users GetUserByLogin(string username, string password, bool rememberMe)
        {
            string hash_password = Crypto.SHA256(password);

            return db.Users.Where(w => w.Username.ToLower().Contains(username.ToLower()) 
                                && w.Password == hash_password)
                                .SingleOrDefault(); ;


        }
    }
}