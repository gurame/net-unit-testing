using System.Data;

namespace BaseApi.Data;

public interface IDbConnectionFactory
{
	Task<IDbConnection> CreateConnectionAsync();
}
