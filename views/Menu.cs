using System;

namespace csharp.views
{
    public class Menu
    {
        protected Menu()
        {
        }

        public static int DisplayAdminMenu()
        {
            Console.WriteLine("-------------------Menu ADMIN-------------------");
            Console.WriteLine("1 - Créer un compte pour un client sans compte");
            Console.WriteLine("2 - Créer un compte utilisateur");
            Console.WriteLine("3 - Désactiver un compte utilisateur");
            Console.WriteLine("4 - Lister les utilisateurs");
            Console.WriteLine("5 - Afficher les comptes utilisateurs actifs ou par rôle");
            Console.WriteLine("6 - Créer un article");
            Console.WriteLine("7 - Afficher un article");
            Console.WriteLine("8 - Filtrer les articles par disponibilité");
            Console.WriteLine("9 - Mettre à jour la quantité en stock");
            Console.WriteLine("10 - Archiver les dettes soldées");
            Console.WriteLine("11 - Quitter");
            Console.Write("Entrez votre choix: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static int DisplayBoutiquierMenu()
        {
            Console.WriteLine("-------------------Menu BOUTIQUIER-------------------");
            Console.WriteLine("1 - Créer un client avec un utilisateur");
            Console.WriteLine("2 - Lister les clients");
            Console.WriteLine("3 - Lister les informations d’un client à partir d'une recherche sur le téléphone");
            Console.WriteLine("4 - Lister les clients ayant un compte ou pas");
            Console.WriteLine("5 - Enregistrer les dettes");
            Console.WriteLine("6 - Payer une dette");
            Console.WriteLine("7 - Lister les dettes non soldées");
            Console.WriteLine("8 - Lister les demandes de dettes en cours ou annuler");
            Console.WriteLine("9 - Accepter ou Annuler une demande de dette");
            Console.WriteLine("10 - Quitter");
            Console.Write("Entrez votre choix: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static int DisplayClientMenu()
        {
            Console.WriteLine("-------------------Menu CLIENT-------------------");
            Console.WriteLine("1 - Lister ses dettes non soldées");
            Console.WriteLine("2 - Faire une demande de dette");
            Console.WriteLine("3 - Lister ses demandes de dette et filtrer par ENCOURS/ANNULER");
            Console.WriteLine("4 - Envoyer une relance pour une demande de dette annulée");
            Console.WriteLine("5 - Quitter");
            Console.Write("Entrez votre choix: ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
