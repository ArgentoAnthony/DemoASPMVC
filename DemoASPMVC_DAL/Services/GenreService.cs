using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using Microsoft.Extensions.Configuration;

namespace DemoASPMVC_DAL.Services
{
    public class GenreService : BaseRepository<Genre>, IGenreService
    {
        public GenreService(IConfiguration config) : base(config)
        {
        }
        protected override Genre Mapper(IDataReader reader)
        {
            return new Genre
            {
                Id = (int)reader["Id"],
                Label = (string)reader["Label"]
            };
        }

        

        public void Create(Genre genre)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "INSERT INTO Genre (Label) " +
                    "VALUES(@Label)";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("Label", genre.Label);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "DELETE FROM Genre WHERE Id = @id";
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("id", id);
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }             
            }
        }
    }
}
