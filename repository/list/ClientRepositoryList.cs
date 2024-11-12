using csharp.core.config;
using csharp.entities;
using csharp.core.interfaces;
using System.Linq;

namespace csharp.repository.list
{
    public class ClientRepositoryList : RepositoryListImpl<Client>, IClientRepository
    {
        private readonly List<Client> list = new();
        public Client SelectByPhone(string phone)
        {
            return list.FirstOrDefault(client => string.Equals(client.Phone, phone, StringComparison.OrdinalIgnoreCase));
        }
        public List<Client> FindAllClientWithAccount()
        {
            return list.Where(client => client.User != null).ToList();
        }


        public Client SelectByUserId(int userId)
        {
            return list.FirstOrDefault(client => client.User != null && client.User.Id == userId);
        }

        public bool InsertClientUser(Client client)
        {
            list.Add(client);
            return true;
        }

    }
}
