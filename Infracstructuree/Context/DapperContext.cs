using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infracstructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        private static readonly object lockObject = new Object();
        private static IDbConnection uniqueInstance;

        public IDbConnection GetConnectionInstance()
        {
            if(uniqueInstance == null)
            {
                lock (lockObject)
                {
                    if(uniqueInstance == null)
                    {
                        uniqueInstance = new SqlConnection(_connectionString);
                    }
                }
            }
            return uniqueInstance;
        }
    }
}
