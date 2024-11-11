using System;
using System.Collections.Generic;
using csharp.core.interfaces;
using csharp.entities;
using csharp.services;

namespace csharp.views
{
    public class ClientView : ViewImpl<Client>
    {
        private readonly ClientServiceImpl clientService;
        private readonly UserServiceImpl userService;
        private readonly UserView userView;

        public ClientView(ClientServiceImpl clientService, UserServiceImpl userService, UserView userView)
        {
            this.clientService = clientService;
            this.userService = userService;
            this.userView = userView;
        }

        public override Client Saisie()
        {
            Client client = new();

            Console.WriteLine("Enter surname: ");
            client.Surname = Console.ReadLine();

            Console.WriteLine("Enter phone: ");
            client.Phone = Console.ReadLine();

            Console.WriteLine("Enter address: ");
            client.Addresse = Console.ReadLine();

            return AskForUserClient(client);
        }

        public Client AskForUserClient(Client client)
        {
            Console.WriteLine("Voulez-vous créer un compte utilisateur pour ce client? (oui/non)");
            string choice = Console.ReadLine();

            if (choice.Equals("oui", StringComparison.OrdinalIgnoreCase))
            {
                User user = userView.Saisie();
                userService.Create(user);
                client.User = user;
            }
            else
            {
                client.User = null;
            }

            return client;
        }
    }
}
