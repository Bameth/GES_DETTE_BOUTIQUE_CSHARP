using System.Data;
using Npgsql;
namespace csharp.core.config
{
    public interface IDataAccess
    {
        NpgsqlConnection GetConnections();
        void CloseConnections();
        NpgsqlConnection Connection { get; }
    }
}