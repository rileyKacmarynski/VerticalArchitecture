using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection _connection;

        public IDbConnection GetOpenConnection()
        {
            if(_connection is null || _connection.State is not ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if(_connection is not null && _connection.State is ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}
