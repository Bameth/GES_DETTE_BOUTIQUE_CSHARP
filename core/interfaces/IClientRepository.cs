using csharp.core.config;
using csharp.entities;

namespace csharp.core.interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client SelectByPhone(String phone);
        List<Client> FindAllClientWithAccount();
        Client SelectByUserId(int userId);
    }
}