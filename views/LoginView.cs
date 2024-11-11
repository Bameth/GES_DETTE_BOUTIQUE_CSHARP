using csharp.entities;
using csharp.services;

namespace csharp.views
{
    public class LoginView
    {
        private readonly UserServiceImpl userServiceImpl;

        public LoginView(UserServiceImpl userServiceImpl)
        {
            this.userServiceImpl = userServiceImpl;
        }

        public User ConnectUser()
        {
            Console.Write("Entrez votre login : ");
            string login = Console.ReadLine()?.Trim();  // Trim input directly
            if (string.IsNullOrEmpty(login))
            {
                Console.WriteLine("Veuillez entrer un login valide.");
                return null;
            }
            Console.Write("Entrez votre mot de passe : ");
            string password = Console.ReadLine()?.Trim();  // Trim input directly
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Veuillez entrer un mot de passe valide.");
                return null;
            }

            User user = userServiceImpl.FindByLogin(login, password);
            if (user != null)
            {
                Console.WriteLine($"Bienvenue, {user.Name} !");
                return user;
            }
            else
            {
                Console.WriteLine("Login ou mot de passe incorrect.");
                return null;
            }
        }

    }
}
