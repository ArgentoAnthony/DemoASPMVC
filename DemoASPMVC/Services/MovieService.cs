using DemoASPMVC.Models;
using System.Data.SqlClient;

namespace DemoASPMVC.Services
{
    public class MovieService : IMovieService
    {
        private readonly string connectionString;

        private readonly SqlConnection _connection;

        public MovieService(SqlConnection connection)
        {
            _connection = connection;
        }
        protected Movie Mapper(SqlDataReader reader)
        {
            return new Movie
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                Description = (string)reader["Description"],
                ImageUrl = reader["ImageUrl"] != DBNull.Value ? (string)reader["ImageUrl"] : null
            };
        }

        public IEnumerable<Movie> GetAll()
        {
            List<Movie> list = new List<Movie>();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "SELECT * FROM dbo.Movie";
                cmd.CommandText = sql;
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(Mapper(reader));
                    }
                }
                _connection.Close();
            }
            return list;

        }


    }
}
