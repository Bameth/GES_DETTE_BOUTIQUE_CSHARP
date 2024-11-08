using csharp.core.interfaces;
using csharp.entities;

namespace csharp.services
{
    public class ClientServiceImpl : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientServiceImpl(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public int Count()
        {
            return clientRepository.Count();
        }

        public void Create(Client objet)
        {
            clientRepository.Insert(objet);
        }

        public List<Client> FindAll()
        {
            return clientRepository.SelectAll();

        }

        public List<Client> FindAllClientWithAccount()
        {
            return clientRepository.FindAllClientWithAccount();
        }

        public Client FindById(int id)
        {
            return clientRepository.SelectById(id);

        }

        public Client FindByUserId(int userId)
        {
            return clientRepository.SelectByUserId(userId);

        }

        public Client GetBy(string objet)
        {
            throw new NotImplementedException();
        }

        public Client Search(string phone)
        {
            return clientRepository.SelectByPhone(phone);

        }

        public bool Update(Client objet)
        {
            return clientRepository.Update(objet);

        }

    }
}