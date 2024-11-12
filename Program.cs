using System;
using csharp.services;
using csharp.views;
using csharp.entities;
using csharp.enums;
using csharp.core.config;
using csharp.core.interfaces;
using csharp.repository.DB;

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

            User loggedInUser = loginView.ConnectUser();

            if (loggedInUser != null)
            {
                if (loggedInUser.Role == Role.ADMIN)
                {
                    do
                    {
                        choice = Menu.DisplayAdminMenu();
                        HandleAdminMenuChoice(choice, clientServiceImpl, userServiceImpl, articleServiceImpl, deptServiceImpl, clientView, deptView, userView, articleView);
                    } while (choice != 11);
                }
                else if (loggedInUser.Role == Role.BOUTIQUIER)
                {
                    do
                    {
                        choice = Menu.DisplayBoutiquierMenu();
                        HandleBoutiquierMenuChoice(choice, clientServiceImpl, userServiceImpl, articleServiceImpl, deptServiceImpl);
                    } while (choice != 10);
                }
                else if (loggedInUser.Role == Role.CLIENT)
                {
                    do
                    {
                        choice = Menu.DisplayClientMenu();
                        HandleClientMenuChoice(choice, deptServiceImpl);
                    } while (choice != 5);
                }
            }
            else
            {
                Console.WriteLine("Échec de la connexion.");
            }
            Console.WriteLine("Programme terminé.");
        }

        // Gestion des actions pour l'Admin
        private static void HandleAdminMenuChoice(int choice, ClientServiceImpl clientServiceImpl,
        UserServiceImpl userServiceImpl, ArticleServiceImpl articleServiceImpl,
        DeptServiceImpl deptServiceImpl, ClientView clientViewImpl, DeptView deptViewImpl, UserView userViewImpl, ArticleView articleView)
        {
            switch (choice)
            {
                case 1:
                    clientServiceImpl.CreateUserclient(clientViewImpl.SaisieClientWithAccount());
                    break;
                case 2:
                    clientServiceImpl.Create(clientViewImpl.Saisie());
                    break;
                case 3:
                    userServiceImpl.Update(userViewImpl.Status());
                    break;
                case 4:
                    foreach (var user in userServiceImpl.FindAll())
                    {
                        Console.WriteLine(user);
                    }
                    break;
                case 5:
                    Console.WriteLine("1- Afficher les utilisateurs actifs/désactivés");
                    Console.WriteLine("2- Afficher les utilisateurs par rôle");
                    int filterChoice = int.Parse(Console.ReadLine());
                    if (filterChoice == 1)
                    {
                        userViewImpl.DisplayUsersByEtat();
                    }
                    else if (filterChoice == 2)
                    {
                        userViewImpl.DisplayUsersByRole();
                    }
                    break;
                case 6:
                    articleServiceImpl.Create(articleView.Saisie());
                    break;
                case 7:
                    foreach (var user in userServiceImpl.FindAll())
                    {
                        Console.WriteLine(user);
                    }
                    break;
                case 8:
                    var availableArticles = articleServiceImpl.FindAvailable();
                    foreach (var article in availableArticles)
                    {
                        Console.WriteLine(article);
                    }
                    break;
                case 9:
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
                case 10:
                    // Archiver les dettes soldées
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }

        // Gestion des actions pour le Boutiquier
        private static void HandleBoutiquierMenuChoice(int choice, ClientServiceImpl clientServiceImpl, UserServiceImpl userServiceImpl, ArticleServiceImpl articleServiceImpl, DeptServiceImpl deptServiceImpl)
        {
            switch (choice)
            {
                case 1:

                    break;
                case 2:
                    // Lister les clients
                    break;
                case 3:
                    // Recherche client par téléphone
                    break;
                case 4:
                    // Lister clients avec ou sans compte
                    break;
                case 5:
                    // Enregistrer les dettes
                    break;
                case 6:
                    // Payer une dette
                    break;
                case 7:
                    // Lister les dettes non soldées
                    break;
                case 8:
                    // Lister les demandes de dettes
                    break;
                case 9:
                    // Accepter/Annuler une demande de dette
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }

        // Gestion des actions pour le Client
        private static void HandleClientMenuChoice(int choice, DeptServiceImpl deptServiceImpl)
        {
            switch (choice)
            {
                case 1:
                    // Lister dettes non soldées
                    break;
                case 2:
                    // Faire une demande de dette
                    break;
                case 3:
                    // Lister demandes de dette et filtrer
                    break;
                case 4:
                    // Relance pour demande annulée
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
    }
}