using Microsoft.Data.Sqlite;
using ModelAgency_Api.Models;

namespace ModelAgency_Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> Login(User user);
    }

    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            string? conStr = _configuration["ConnectionString"];
            if (!string.IsNullOrEmpty(conStr))
            {
                connectionString = conStr;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<User> Login(User user)
        {
            const string getUser = @"SELECT * from User
                                             WHERE User.Email = @Email AND User.Password = @Password";

            User returnUser = new User();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = getUser;

                command.Parameters.Add("@Email", SqliteType.Text).Value = user.Email;
                command.Parameters.Add("@Password", SqliteType.Text).Value = user.Password;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        returnUser.Id = reader.GetInt32(0);
                        returnUser.Email = reader.GetString(1);
                        returnUser.Password = string.Empty;
                        returnUser.Name = reader.GetString(3);
                        returnUser.Surname = reader.GetString(4);
                        returnUser.DateOfBith = DateTime.Parse(reader.GetString(5));
                        returnUser.GenderId = reader.GetInt32(6);
                        returnUser.RoleId = reader.GetInt32(7);
                    }
                }
            }

            return returnUser;
        }
    }
}
