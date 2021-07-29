using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DapperCRUD.Core.Persistence.DapperConnection
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection connection;
        private readonly IOptions<ConnectionConfiguration> configs;

        public FactoryConnection(IOptions<ConnectionConfiguration> configs)
        {
            this.configs = configs;
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(configs.Value.ConnectionDB);
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
