using System;
using System.Collections.Generic;
using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;
using csharp.services;

namespace csharp.views
{
    public class UserView : ViewImpl<User>
    {
        private readonly UserServiceImpl userService;

        public UserView(UserServiceImpl userService)
        {
            this.userService = userService;
        }

        public override User Saisie()
        {
            User user = new User();
            Console.WriteLine("Veuillez saisir les informations de l'utilisateur :");
            Console.WriteLine("===============================================");
            Console.WriteLine("Name :");
            user.Name = Console.ReadLine();

            Console.WriteLine("Login :");
            user.Login = Console.ReadLine();

            Console.WriteLine("Password :");
            user.Password = Console.ReadLine();

            Console.WriteLine("==============================================");
            user.Role = SaisieRole();
            user.TypeEtat = TypeEtat.ACTIVER;

            return user;
        }

        public User Status()
        {
            Console.WriteLine("Veuillez saisir le nom de l'utilisateur à activer/désactiver son compte : ");
            string userName = Console.ReadLine();
            User user = userService.GetBy(userName);

            if (user == null)
            {
                Console.WriteLine("L'utilisateur avec le nom spécifié n'existe pas.");
                return null;
            }

            user.TypeEtat = SaisieEtat();
            if (userService.Update(user))
            {
                Console.WriteLine("Status du compte changé avec succès !");
            }
            else
            {
                Console.WriteLine("Erreur lors du changement de statut du compte.");
            }

            return user;
        }

        public Role SaisieRole()
        {
            int roleChoice;
            do
            {
                foreach (Role role in Enum.GetValues(typeof(Role)))
                {
                    Console.WriteLine($"{(int)role + 1}-{role}");
                }
                Console.WriteLine("Veuillez sélectionner un rôle : ");
                roleChoice = int.Parse(Console.ReadLine());
            } while (roleChoice <= 0 || roleChoice > Enum.GetValues(typeof(Role)).Length);

            return (Role)(roleChoice - 1);
        }

        public TypeEtat SaisieEtat()
        {
            int etatChoice;
            do
            {
                foreach (TypeEtat etat in Enum.GetValues(typeof(TypeEtat)))
                {
                    Console.WriteLine($"{(int)etat + 1}-{etat}");
                }
                Console.WriteLine("Veuillez sélectionner un État : ");
                etatChoice = int.Parse(Console.ReadLine());
            } while (etatChoice <= 0 || etatChoice > Enum.GetValues(typeof(TypeEtat)).Length);

            return (TypeEtat)(etatChoice - 1);
        }

        public void DisplayUsersByEtat()
        {
            Console.WriteLine("Sélectionner l'état des utilisateurs à afficher:");
            TypeEtat typeEtat = SaisieEtat();
            List<User> users = userService.FindByEtat(typeEtat);

            if (users.Count == 0)
            {
                Console.WriteLine("Aucun utilisateur trouvé avec cet état.");
            }
            else
            {
                users.ForEach(Console.WriteLine);
            }
        }

        public void DisplayUsersByRole()
        {
            Console.WriteLine("Sélectionner le rôle des utilisateurs à afficher:");
            Role role = SaisieRole();
            List<User> users = userService.FindByRole(role);

            if (users.Count == 0)
            {
                Console.WriteLine("Aucun utilisateur trouvé avec ce rôle.");
            }
            else
            {
                users.ForEach(Console.WriteLine);
            }
        }

        public void DisplayUsers(List<User> users)
        {
            if (users.Count == 0)
            {
                Console.WriteLine("Aucun utilisateur trouvé.");
            }
            else
            {
                users.ForEach(Console.WriteLine);
            }
        }
    }
}