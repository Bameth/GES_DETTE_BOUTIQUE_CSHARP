using System;
using csharp.core.interfaces;
using csharp.entities;
using csharp.services;

namespace csharp.views
{
    public class ArticleView : ViewImpl<Articles>
    {
        private readonly ArticleServiceImpl articleService;

        public ArticleView(ArticleServiceImpl articleService) {
            this.articleService = articleService;
        }

        public Articles UpdateQteStock() { 
            Console.WriteLine("Veuillez saisir la référence de l'article : ");
            Console.WriteLine("======================================================");
            string reference = Console.ReadLine();
            Articles article = articleService.GetBy(reference);

            if (article == null) {
                Console.WriteLine("Article inexistant");
                return null;
            }
            Console.WriteLine("Mise à jour de la quantité de stock de l'article :");
            Console.Write("Nouveau QteStock: ");
            int qteStock = int.Parse(Console.ReadLine());
            article.QteStock = qteStock;
            return article;
        }

        public override Articles Saisie()
        {
            Articles article = new Articles();
            Console.WriteLine("Veuillez saisir les informations de l'article :");
            Console.WriteLine("======================================================");
            Console.WriteLine("Libellé :");
            string libelle = Console.ReadLine();
            article.Libelle = libelle;
            Console.WriteLine("Prix :");
            double prix = double.Parse(Console.ReadLine());
            article.Prix = prix;
            Console.WriteLine("Quantité en stock :");
            int qteStock = int.Parse(Console.ReadLine());
            article.QteStock = qteStock;
            Console.WriteLine("======================================================");
            return article;
        }
    }
}
