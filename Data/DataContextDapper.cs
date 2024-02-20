using Dapper;
using Npgsql;

namespace FusionMapAPI.Data
{
    public class DataContextDapper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public IEnumerable<T> LoadData<T>(string sql, DynamicParameters? dynamicParameters = null)
        {
            NpgsqlConnection connection = new(_config.GetConnectionString("DefaultConnection"));
            return connection.Query<T>(sql, dynamicParameters);
        }

        public T LoadDataSingle<T>(string sql, DynamicParameters? dynamicParameters = null)
        {
            NpgsqlConnection connection = new(_config.GetConnectionString("DefaultConnection"));
            return connection.QuerySingle<T>(sql, dynamicParameters);
        }

        public bool ExecuteSql(string sql, DynamicParameters? dynamicParameters = null)
        {
            NpgsqlConnection connection = new(_config.GetConnectionString("DefaultConnection"));
            return connection.Execute(sql, dynamicParameters) > 0;
        }

        public int ExecuteSqlWithRowsAffected(string sql, DynamicParameters? dynamicParameters = null)
        {
            NpgsqlConnection connection = new(_config.GetConnectionString("DefaultConnection"));
            return connection.Execute(sql, dynamicParameters);
        }
    }
}
