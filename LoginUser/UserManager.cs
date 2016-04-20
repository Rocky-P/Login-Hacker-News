using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginUser
{
    public class UserManager
    {
        private string _connectionString;
        public UserManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User AddUser(string userName, string password)
        {
            //we should really check if username exists.....
            using (UserDataContext dataContext = new UserDataContext())
            {

                User user = new User();
                user.Salt = PasswordHelper.GenerateSalt();
                user.UserName = userName;
                user.HashPassword = PasswordHelper.HashPassword(password, user.Salt);


                dataContext.Users.InsertOnSubmit(user);


                dataContext.SubmitChanges();

                //user.Id = (int)(decimal)cmd.ExecuteScalar();

                return user;
            }
        }

        public int GetUserId(int Id)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
                User user = dataContext.Users.FirstOrDefault(u => u.Id == Id);

                return user.Id;

            }
        }

        public IEnumerable<User> GetAllUser()
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
               

                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Link>(l => l.Uploads);
                loadOptions.LoadWith<User>(u=>u.Links);
                dataContext.LoadOptions = loadOptions;


                IEnumerable<User> Users = dataContext.Users.ToList();
                return Users;
            }
        }


        public int GetUserbyUsername(string username)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<User>(u => u.Links);
                dataContext.LoadOptions = loadOptions;

                User user = dataContext.Users.FirstOrDefault(u => u.UserName == username);

                return user.Id;
            }
        }

        public User GetUserbyUsernameReturnUser(string username)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Link>(l => l.Uploads);
                loadOptions.LoadWith<User>(u => u.Links);
                dataContext.LoadOptions = loadOptions;

                User user = dataContext.Users.FirstOrDefault(u => u.UserName == username);

                return user;
            }
        }
        public User GetUser(string username, string password)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Link>(l => l.Uploads);
                loadOptions.LoadWith<User>(u => u.Links);
                dataContext.LoadOptions = loadOptions;

                User user = dataContext.Users.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    return null;
                }
                bool correctPassword = PasswordHelper.PasswordMatch(password, user.HashPassword, user.Salt);
                return correctPassword ? user : null; //ternary operator
            }
        }

        public bool UserAutherntification(string username, string password)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
                User u = dataContext.Users.FirstOrDefault(i => i.UserName == username);
                bool correctPassword = PasswordHelper.PasswordMatch(password, u.HashPassword, u.Salt);
                return correctPassword ? true : false; //ternary operator

            }
        }

        public static class PasswordHelper
        {
            public static string GenerateSalt()
            {
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                byte[] bytes = new byte[15];
                provider.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }

            public static string HashPassword(string password, string salt)
            {
                SHA256Managed managed = new SHA256Managed();
                byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
                byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
                byte[] combined = passwordBytes.Concat(saltBytes).ToArray();
                return Convert.ToBase64String(managed.ComputeHash(combined));
            }

            public static bool PasswordMatch(string input, string passwordHash, string salt)
            {
                string inputHash = HashPassword(input, salt);
                return inputHash == passwordHash;
            }

        }
    }
}

