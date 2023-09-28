using System.Data.SqlClient;
using DemoASPMVC_DAL.Models;
using DemoASPMVC.Mapper;
using DemoASPMVC.Models.ViewModel;
using Humanizer.Localisation;

namespace DemoASPMVC.Services
{
    public class UserDBService : IUserDBService
    {
        private readonly string connectionString;

        private readonly SqlConnection _connection;

        private User Mapper(UserRegisterForm u)
        {
            return u.ToUser();
            
        }

        private UserRegisterForm Mapper(User u)
        {
            return u.ToRegisterDTO();
        }
        protected User Mapper(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Nickname = (string)reader["Nickname"],
                Email = (string)reader["Email"],
            };
        }
        public UserDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Create(UserRegisterForm u)
        {
            User user = Mapper(u);
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "INSERT INTO dbo.UsersDB VALUES (@nom,@mail,@pass)";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@nom", user.Nickname);
                cmd.Parameters.AddWithValue("@mail", user.Email);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public User Update(int id, UserRegisterForm u)
        {
            User user = Mapper(u);
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "UPDATE dbo.UsersDB SET Nickname = @nick, Email =@email, Password=@pass WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("nick", user.Nickname);
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.CommandText = sql;
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return user;
        }
        public User GetUsersById(int id)
        {
            User user = null;
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "SELECT * FROM dbo.UsersDB WHERE Id =@id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandText = sql;
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = Mapper(reader);
                    }
                }
                _connection.Close();
            }
            return user;
        }
        public UserRegisterForm GetUsersByIdForm(int id)
        {
            return Mapper(GetUsersById(id));
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "SELECT * FROM dbo.UsersDB";
                cmd.CommandText= sql;
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(Mapper(reader));
                    }
                }
                _connection.Close();
            }
            return users;
        }
        public void Delete(int id)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "DELETE FROM dbo.UsersDB WHERE Id=@id";
                cmd.CommandText= sql;
                cmd.Parameters.AddWithValue("id", id);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

    }
}
