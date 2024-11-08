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
            InitializeUsers();
        }
        private void InitializeUsers()
        {
            if (userRepository.Count() == 0)
            {
                userRepository.Insert(new User
                {
                    Name = "BAILA WANE",
                    Login = "bbw_login",
                    Password = "b".Trim(),
                    Role = Role.ADMIN,
                    TypeEtat = TypeEtat.ACTIVER
                });

                userRepository.Insert(new User
                {
                    Name = "Boutiquier User",
                    Login = "bobo_login",
                    Password = "b".Trim(),
                    Role = Role.BOUTIQUIER,
                    TypeEtat = TypeEtat.ACTIVER
                });

                userRepository.Insert(new User
                {
                    Name = "Client User",
                    Login = "moh_login",
                    Password = "b".Trim(),
                    Role = Role.CLIENT,
                    TypeEtat = TypeEtat.ACTIVER
                });
            }

            foreach (var user in userRepository.SelectAll())
            {
                Console.WriteLine($"User {user.Login} - {user.Name} - {user.Role} - {user.TypeEtat}");
                Console.WriteLine($"Utilisateur créé : {user.Login}, Password: {user.Password}");
            }
        }


        public int Count()
        {
            return userRepository.Count();
        }

        public void Create(User objet)
        {
            userRepository.Insert(objet);
        }

        public List<User> FindAll()
        {
            return userRepository.SelectAll();
        }

        public List<User> FindByEtat(TypeEtat etat)
        {
            return userRepository.SelectByEtat(etat);
        }

        public User FindByLogin(string login, string password)
        {
            Console.WriteLine($"Tentative de connexion : Login={login}, Password={password}");
            return userRepository.SelectByLogin(login, password);
        }


        public List<User> FindByRole(Role role)
        {
            return userRepository.SelectByRole(role);
        }

        public User GetBy(string objet)
        {
            return userRepository.SelectByName(objet);
        }

        public bool Update(User objet)
        {
            return userRepository.Update(objet);
        }
    }
}