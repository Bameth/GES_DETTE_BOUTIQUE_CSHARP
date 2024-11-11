using csharp.core.config;
using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;
using csharp.repository.DB;
using csharp.repository.list;
using csharp.services;
using csharp.views;

namespace csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IDataAccess dataAccess = new DataAccess();
            IClientRepository clientRepository = new ClientRepositoryBdImpl(dataAccess);
            IDeptRepository deptRepository = new DetteRepositoryBdImpl(dataAccess);
            IArticleRepository articleRepository = new ArticlesRepositoryBdImpl(dataAccess);
            IDetailRepository detailRepository = new DetailRepositoryBdImpl(dataAccess);
            IUserRepository userRepository = new UserRepositoryBdImpl(dataAccess);
            ClientServiceImpl clientServiceImpl = new(clientRepository);
            UserServiceImpl userServiceImpl = new(userRepository);
            ArticleServiceImpl articleServiceImpl = new(articleRepository);
            DetailServiceImpl detailServiceImpl = new(detailRepository);
            DeptServiceImpl deptServiceImpl = new(deptRepository);
            UserView userView = new(userServiceImpl);
            ClientView clientView = new(clientServiceImpl, userServiceImpl, userView);
            ArticleView articleView = new(articleServiceImpl);
            DeptView deptView = new(clientServiceImpl, deptServiceImpl, articleServiceImpl, detailServiceImpl);
            LoginView loginView = new(userServiceImpl);
            int choice;

            // User loggedInUser = loginView.ConnectUser();

            // if (loggedInUser != null)
            // {
            //     if (loggedInUser.Role == Role.ADMIN)
            //     {
            //         Menu.DisplayAdminMenu();
            //     }
            //     else if (loggedInUser.Role == Role.BOUTIQUIER)
            //     {
            //         Menu.DisplayBoutiquierMenu();
            //     }
            //     else if (loggedInUser.Role == Role.CLIENT)
            //     {
            //         Menu.DisplayClientMenu();
            //     }
            // }
            do
            {
                Console.WriteLine("1- Create client");
                Console.WriteLine("2- List client");
                Console.WriteLine("3- Find client by phone");
                Console.WriteLine("4- Create User(Boutiquier/Admin)");
                Console.WriteLine("5- List User");
                Console.WriteLine("6- Desactiver/Activer un compte utilisateur");
                Console.WriteLine("7- Afficher les comptes utilisateurs actifs ou par rôle");
                Console.WriteLine("8- Créer un Article");
                Console.WriteLine("9- Lister tous les Articles");
                Console.WriteLine("10- Lister les Articles disponibles");
                Console.WriteLine("11- Update qteStock");
                Console.WriteLine("12- Archiver les Dettes soldés");
                Console.Write("Choisissez une option: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        clientServiceImpl.Create(clientView.Saisie());
                        break;

                    case 2:
                        var listClients = clientServiceImpl.FindAll();
                        foreach (var client in listClients)
                        {
                            Console.WriteLine(client);
                        }
                        break;

                    case 3:
                        // Implementation for finding client by phone
                        break;

                    case 4:
                        userServiceImpl.Create(userView.Saisie());
                        break;

                    case 5:
                        var listUsers = userServiceImpl.FindAll();
                        foreach (var user in listUsers)
                        {
                            Console.WriteLine(user);
                        }
                        break;

                    case 6:
                        userServiceImpl.Update(userView.Status());
                        break;

                    case 7:
                        Console.WriteLine("1- Afficher les utilisateurs actifs/désactivés");
                        Console.WriteLine("2- Afficher les utilisateurs par rôle");
                        int filterChoice = int.Parse(Console.ReadLine());
                        if (filterChoice == 1)
                        {
                            userView.DisplayUsersByEtat();
                        }
                        else if (filterChoice == 2)
                        {
                            userView.DisplayUsersByRole();
                        }
                        break;

                    case 8:
                        articleServiceImpl.Create(articleView.Saisie());
                        break;

                    case 9:
                        var listArticles = articleServiceImpl.FindAll();
                        foreach (var article in listArticles)
                        {
                            Console.WriteLine(article);
                        }
                        break;

                    case 10:
                        var availableArticles = articleServiceImpl.FindAvailable();
                        foreach (var article in availableArticles)
                        {
                            Console.WriteLine(article);
                        }
                        break;

                    case 11:
                        var articleToUpdate = articleView.UpdateQteStock();
                        if (articleToUpdate != null)
                        {
                            if (articleServiceImpl.Update(articleToUpdate))
                            {
                                Console.WriteLine("qteStock modifiée avec succès !");
                            }
                            else
                            {
                                Console.WriteLine("Erreur lors de la modification de la qteStock.");
                            }
                        }
                        break;

                    case 12:
                        // Implement archiving debts
                        break;

                    default:
                        Console.WriteLine("Option non valide. Veuillez réessayer.");
                        break;
                }
            } while (choice != 13);

            Console.WriteLine("Programme terminé.");

        }

    }
}
