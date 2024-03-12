using Microsoft.Data.SqlClient;

namespace Vision.Database
{
    public class SqlConnector
    {
        public static SqlConnection? sqlConnection;
        public SqlConnector(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }
    }
}
