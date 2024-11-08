using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;
using csharp.repository.list;
using csharp.services;
using csharp.views;

namespace csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IClientRepository clientRepository = new ClientRepositoryList();
            IDeptRepository deptRepository = new DetteRepositoryListImpl();
            IArticleRepository articleRepository = new ArticleRepositoryList();
            IDetailRepository detailRepository = new DetailRepositoryList();
            IUserRepository userRepository = new UserRepositoryList();
            ClientServiceImpl clientServiceImpl = new(clientRepository);
            UserServiceImpl userServiceImpl = new(userRepository);
            ArticleServiceImpl articleServiceImpl = new(articleRepository);
            DetailServiceImpl detailServiceImpl = new(detailRepository);
            DeptServiceImpl deptServiceImpl = new(deptRepository);
            UserView userView = new(userServiceImpl);
            ClientView clientView = new(clientServiceImpl, userServiceImpl, userView);
            ArticleView articleView = new(articleServiceImpl);
            DeptView deptView = new(clientServiceImpl, deptServiceImpl, articleServiceImpl, detailServiceImpl);
            LoginView loginView = new LoginView(userServiceImpl);
            int choice;

            User loggedInUser = loginView.ConnectUser();

            if (loggedInUser != null)
            {
                if (loggedInUser.Role == Role.ADMIN)
                {
                    Menu.DisplayAdminMenu();
                }
                else if (loggedInUser.Role == Role.BOUTIQUIER)
                {
                    Menu.DisplayBoutiquierMenu();
                }
                else if (loggedInUser.Role == Role.CLIENT)
                {
                    Menu.DisplayClientMenu();
                }
            }
        }

    }
}
