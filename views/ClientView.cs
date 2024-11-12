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
            client.Address = Console.ReadLine();


            return client;
        }
        public Client SaisieClientWithAccount()
        {
            Client client = new();

            Console.WriteLine("Enter surname: ");
            client.Surname = Console.ReadLine();

            Console.WriteLine("Enter phone: ");
            client.Phone = Console.ReadLine();

            Console.WriteLine("Enter address: ");
            client.Address = Console.ReadLine();

            User user = userView.Saisie();
            client.User = user;

            return client;
        }
    }
}
