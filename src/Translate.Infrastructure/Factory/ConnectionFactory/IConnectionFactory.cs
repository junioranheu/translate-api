using MySqlConnector;

namespace Translate.Infrastructure.Factory.ConnectionFactory
{
    public interface IConnectionFactory
    {
        MySqlConnection ObterMySqlConnection();
        string ObterStringConnection();
    }
}