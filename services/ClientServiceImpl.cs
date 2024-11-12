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

        public bool Create(Client objet)
        {
            return clientRepository.Insert(objet);
        }

        public bool CreateUserclient(Client objet)
        {
            return clientRepository.InsertClientUser(objet);
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
            return clientRepository.SelectByPhone(objet);
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