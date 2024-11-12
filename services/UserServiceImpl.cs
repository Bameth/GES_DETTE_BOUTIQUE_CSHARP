using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;

namespace csharp.services
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserServiceImpl(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public int Count()
        {
            return userRepository.Count();
        }

        public bool Create(User objet)
        {
            return userRepository.Insert(objet);
        }

        public List<User> FindAll()
        {
            return userRepository.SelectAll();
        }

        public List<User> FindByEtat(TypeEtat etat)
        {
            return userRepository.SelectByEtat(etat);
        }

        public User? FindByLogin(string login, string password)
        {
            Console.WriteLine($"Tentative de connexion : Login={login}, Password={password}");
            return userRepository.SelectByLogin(login, password);
        }


        public List<User> FindByRole(Role role)
        {
            return userRepository.SelectByRole(role);
        }

        public User? GetBy(string objet)
        {
            return userRepository.SelectByName(objet);
        }

        public bool Update(User objet)
        {
            return userRepository.Update(objet);
        }
    }
}