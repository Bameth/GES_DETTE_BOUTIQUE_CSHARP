namespace csharp.services
{
    using csharp.core.interfaces;
    using csharp.entities;
    public interface IClientService : IService<Client>
    {
        public Client Search(String phone);
        bool CreateUserclient(Client objet);

        public Client FindById(int id);
        List<Client> FindAllClientWithAccount();
        Client FindByUserId(int userId);
    }
}