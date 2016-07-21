using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(string username, string password)
        {
            string salt = PasswordHelper.GenerateRandomSalt();
            string hash = PasswordHelper.HashPassword(password, salt);
            User user = new User
            {
                UserName = username,
                PasswordSalt = salt,
                PasswordHash = hash,
            };
            using(var context = new ECommerceDbDataContext(_connectionString))
            {                
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }
        }

        public User Login(string username, string password)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                User user = context.Users.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    return null;
                }

                if (!PasswordHelper.PasswordMatch(password, user.PasswordSalt, user.PasswordHash))
                {
                    return null;
                }

                return user;
            }
        }
        //public User GetUser(string username)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        var command = connection.CreateCommand();
        //        command.CommandText = "Select * FROM Users WHERE Username = @username";
        //        command.Parameters.AddWithValue("@username", username);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        reader.Read();

        //        return new User
        //        {
        //            Id = (int)reader["Id"],
        //            Username = (string)reader["Username"],
        //            PasswordHash = (string)reader["PasswordHash"],
        //            PasswordSalt = (string)reader["PasswordSalt"],
        //        };

        //    }
        //}
    }
}
