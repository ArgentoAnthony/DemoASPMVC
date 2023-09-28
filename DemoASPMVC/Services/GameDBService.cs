using DemoASPMVC.Models;
using System.Data.SqlClient;

namespace DemoASPMVC.Services
{
    public class GameDBService : IGameService
    {
        private readonly string connectionString;

        private readonly SqlConnection _connection;

        //public GameDBService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("default");
        //    _connection = new SqlConnection(connectionString);
        //}

        public GameDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        protected Game Mapper(SqlDataReader reader)
        {
            return new Game
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                Description = (string)reader["Description"],
                Genre = (string)reader["Genre"]
            };
        }

        
        public void Create(Game game)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Game (Title, Description, Genre) " +
                    "VALUES(@title, @desc, @genre)";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("title", game.Title);
                cmd.Parameters.AddWithValue("desc", game.Description);
                cmd.Parameters.AddWithValue("genre", game.Genre);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "DELETE FROM Game WHERE Id = @id";
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public Game GetById(int id)
        {
            Game game = new();
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Game WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read()) game = Mapper(reader);
                }
                _connection.Close();
            }
            return game;
        }

        public IEnumerable<Game> GetGames()
        {
            List<Game> game = new List<Game>();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Game";
                
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        game.Add(Mapper(reader));
                    }
                }
                _connection.Close();
            }
            return game;
        }
    }
}
