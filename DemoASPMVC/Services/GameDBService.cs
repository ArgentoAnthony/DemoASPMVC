using DemoASPMVC.Models;
using DemoASPMVC_DAL.Models;
using System.Data.SqlClient;
using DemoASPMVC_DAL.Services;
using DemoASPMVC.Services;
using Microsoft.AspNetCore.Authorization;
using DemoASPMVC.Tools;
using DemoASPMVC_DAL.Interface;

namespace DemoASPMVC.Services
{
    public class GameDBService : IGameService
    {
        private readonly IGenreService _genreService;
        private readonly SessionManager _session;
        private readonly string connectionString;

        private readonly SqlConnection _connection;

        //public GameDBService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("default");
        //    _connection = new SqlConnection(connectionString);
        //}

        public GameDBService(SqlConnection connection, IGenreService genreService, SessionManager session)
        {
            _connection = connection;
            _genreService = genreService;
            _session = session;
        }

        protected Game Mapper(SqlDataReader reader)
        {
            return new Game
            {
                Id = (int)reader["Id"],  
                Title = (string)reader["Title"],
                Description = (string)reader["Description"],
               // Genre_Id = reader["Genre_Id"] != DBNull.Value ? (int)reader["Genre_Id"] : null,
                Label = (string)reader["Label"]
            };
        }
        public void Create(Game game)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Game (Title, Description, Genre_Id) " +
                    "VALUES(@title, @desc, @genre)";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("title", game.Title);
                cmd.Parameters.AddWithValue("desc", game.Description);
                cmd.Parameters.AddWithValue("genre", game.Genre_Id);

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
        public IEnumerable<Game> GetGamesByGenre(int genreId)
        {
            List<Game> game = new List<Game>();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql;
                if (genreId > 0)
                {
                    sql = "SELECT * FROM Game JOIN Genre ON Game.Genre_Id = Genre.Id WHERE Genre.Id = @id";
                    cmd.Parameters.AddWithValue("id", genreId);
                    
                }
                else
                {
                    sql = "SELECT * FROM Game JOIN Genre ON Game.Genre_Id = Genre.Id";
                    
                }
                cmd.CommandText = sql;
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        game.Add(Mapper(reader));
                    }
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

                cmd.CommandText = "SELECT * FROM Game JOIN Genre ON Game.Genre_Id = Genre.Id";
                
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

        public void AddFavorite(int id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Favoris VALUES (@idJeu, @idUser);";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("idJeu", id);
                cmd.Parameters.AddWithValue("idUser", _session.ConnectedUser.Id);
                if (_connection.State == System.Data.ConnectionState.Open) _connection.Close();

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void RemoveFavorite(int id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "DELETE FROM Favoris WHERE Id_Jeu = @idJeu AND Id_User = @idUser;";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("idJeu", id);
                cmd.Parameters.AddWithValue("idUser", _session.ConnectedUser.Id);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public bool IsFavoris(int idJeu, int idUser)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Favoris WHERE Id_Jeu = @idJeu AND Id_User = @idUser";
                cmd.Parameters.AddWithValue("idJeu", idJeu);
                cmd.Parameters.AddWithValue("idUser", idUser);
                _connection.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine(reader);
                    if (reader.Read())
                    {
                        Console.WriteLine("test");
                        return true;
                    }
                    return false;
                }
            }
        }
        public IEnumerable<Game> GetFavoris()
        {
            List<Game> game = new List<Game>();
            using (SqlCommand cmd = _connection.CreateCommand())
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "ProcFavoris";

                cmd.Parameters.AddWithValue("id", _session.ConnectedUser.Id);

                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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
