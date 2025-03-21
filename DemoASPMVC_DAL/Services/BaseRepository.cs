﻿using DemoASPMVC_DAL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPMVC_DAL.Services
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel>
    {
        protected readonly string _connectionString;
        public BaseRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Techni");
        }
        protected abstract TModel Mapper(IDataReader reader);
        public virtual IEnumerable<TModel> GetAll(string tablename)
        {

            List<TModel> list = new List<TModel>();
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                cnx.Open();
                string sql = "SELECT * FROM " + tablename;

                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(Mapper(reader));
                        }
                    }
                }
                return list;
            }
        }
        public virtual TModel GetById(string tablename, int id)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {              
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cnx.Open();
                    string sql = $"SELECT * FROM {tablename} WHERE Id = @id";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            reader.Read();
                            return Mapper(reader);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            return Mapper(reader);
                        }
                    }
                }
            }
        }

        public virtual void Delete(string tablename, int id)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = $"DELETE FROM {tablename} WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandText = sql;
                }
            }
        }
    }
}
