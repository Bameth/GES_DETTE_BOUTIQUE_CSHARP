using System;
using System.Collections.Generic;
using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;
using csharp.services;

namespace csharp.views
{
    public class DeptView : ViewImpl<Dept>
    {
        private readonly ClientServiceImpl clientService;
        private readonly DeptServiceImpl deptService;
        private readonly ArticleServiceImpl articleService;
        private readonly DetailServiceImpl detailService;

        public DeptView(ClientServiceImpl clientService, DeptServiceImpl deptService,
            ArticleServiceImpl articleService, DetailServiceImpl detailService)
        {
            this.clientService = clientService;
            this.deptService = deptService;
            this.articleService = articleService;
            this.detailService = detailService;
        }


        public override Dept Saisie()
        {
            Console.WriteLine("========================");
            Client client = AskDeptClient();
            if (client == null)
            {
                Console.WriteLine("Client introuvable.");
                return null;
            }
            Console.WriteLine("=============================");
            Articles article = AskDeptArticle();
            if (article == null)
            {
                Console.WriteLine("L'article avec cette référence n'existe pas.");
                return null;
            }
            Console.WriteLine("=============================");
            Console.Write("Entrez la quantité de l'article: ");
            int qte = int.Parse(Console.ReadLine());
            if (qte <= 0 || qte > article.QteStock)
            {
                Console.WriteLine("La quantité saisie est incorrecte ou dépasse le stock disponible.");
                return null;
            }

            double montant = article.Prix * qte;
            Console.WriteLine("===============================");
            Console.WriteLine("Le montant total de la dette est: " + montant);
            Console.WriteLine("===============================");

            Console.Write("Entrez le montant versé: ");
            double montantVerser = double.Parse(Console.ReadLine());
            double montantRestant = montant - montantVerser;

            if (montantRestant < 0)
            {
                Console.WriteLine("Le montant versé est supérieur au montant de la dette.");
                return null;
            }

            Dept dept = new()
            {
                Montant = montant,
                MontantVerser = montantVerser,
                Client = client
            };
            deptService.Create(dept);

            Detail detail = new()
            {
                Articles = article,
                Qte = qte,
                Dept = dept
            };
            dept.Details.Add(detail);

            article.QteStock -= qte;
            articleService.Update(article);
            detailService.Create(detail);

            Console.WriteLine("👌👌👌👌👌👌👌👌👌👌👌👌👌👌👌");
            Console.WriteLine("La dette a été enregistrée avec succès.");
            return dept;
        }

        public Articles AskDeptArticle()
        {
            Console.Write("Entrez la référence de l'article: ");
            string reference = Console.ReadLine();
            return articleService.GetBy(reference);
        }

        public Client AskDeptClient()
        {
            Console.Write("Entrez le numéro de téléphone du client: ");
            string phone = Console.ReadLine();
            return clientService.Search(phone);
        }

        public Dept PayementDept()
        {
            Console.Write("Entrez le numéro de téléphone du client dont vous voulez payer la dette: ");
            string phone = Console.ReadLine();
            Client client = clientService.Search(phone);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return null;
            }
            List<Dept> debts = deptService.FindDebtsByClientId(client.Id);
            if (debts.Count == 0)
            {
                Console.WriteLine("Ce client n'a pas de dettes.");
                return null;
            }
            Console.WriteLine("Dettes du client :");
            foreach (var dept in debts)
            {
                Console.WriteLine(dept);
            }
            Console.Write("Entrez l'id de la dette à payer: ");
            int id = int.Parse(Console.ReadLine());
            Dept deptToPay = debts.Find(d => d.Id == id);
            if (deptToPay == null)
            {
                Console.WriteLine("Dette non trouvée ou ne fait pas partie des dettes du client.");
                return null;
            }
            Console.WriteLine("Détails de la dette : " + deptToPay);
            Console.Write("Entrez le montant à verser: ");
            double montantVerser = double.Parse(Console.ReadLine());
            double montantRestant = deptToPay.MontantRestant - montantVerser;
            if (montantRestant < 0)
            {
                Console.WriteLine("Le montant versé est supérieur au montant restant de la dette.");
                return null;
            }
            deptToPay.MontantVerser += montantVerser;
            deptToPay.UpdateEtat();
            deptService.Update(deptToPay);

            Console.WriteLine(montantRestant <= 0
                ? "La dette a été entièrement réglée."
                : "La dette a été partiellement réglée.");
            Console.WriteLine("Mise à jour effectuée avec succès.");
            return deptToPay;
        }

        public void DisplayDebtsByEtat()
        {
            Console.WriteLine("Sélectionner l'état de la dette à afficher:");
            TypeDette typeDette = SaisieEtat();
            List<Dept> depts = deptService.FindByEtat(typeDette);

            if (depts.Count == 0)
            {
                Console.WriteLine("Aucune dette trouvée avec cet état.");
            }
            else
            {
                depts.ForEach(Console.WriteLine);
            }
        }

        public TypeDette SaisieEtat()
        {
            int etatChoice;
            do
            {
                foreach (TypeDette etat in Enum.GetValues(typeof(TypeDette)))
                {
                    Console.WriteLine((int)etat + 1 + "-" + etat.ToString());
                }
                Console.WriteLine("Veuillez sélectionner un Etat : ");
                etatChoice = int.Parse(Console.ReadLine());
            } while (etatChoice <= 0 || etatChoice > Enum.GetValues(typeof(TypeDette)).Length);

            return (TypeDette)(etatChoice - 1);
        }

        public void RelancerDetteAnnulee()
        {
            List<Dept> canceledDebts = deptService.FindByEtat(TypeDette.ANNULER);
            Console.Write("Entrez l'id de la dette à relancer : ");
            int id = int.Parse(Console.ReadLine());
            Dept deptToRelancer = canceledDebts.Find(d => d.Id == id);

            Console.Write("Êtes-vous sûr de vouloir relancer cette dette ? (oui/non) : ");
            string confirmation = Console.ReadLine();
            if (!confirmation.Equals("oui", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Demande de relance annulée.");
                return;
            }

            deptToRelancer.TypeDette = TypeDette.ENCOURS;
            deptService.Update(deptToRelancer);
            Console.WriteLine("La demande de relance a été effectuée avec succès.");
        }
    }
}
