using System.Data;

namespace DapperCRUD.Core.Persistence.DapperConnection
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}
