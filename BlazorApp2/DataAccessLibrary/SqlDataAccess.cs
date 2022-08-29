using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration config;

        public string ConnectionStringName { get; set; } = "Default";


        public SqlDataAccess(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parametres)
        {
            string connectionString = this.config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parametres);
                return data.ToList();
            }

        }
        public async Task SaveData<T>(string sql, T parametres)
        {
            string connectionString = this.config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.ExecuteAsync(sql, parametres);
            }
        }
    }
}
