using csharp.core.config;
using csharp.entities;
using csharp.core.interfaces;
namespace csharp.repository.list
{
    public class ArticleRepositoryList : RepositoryListImpl<Articles>, IArticleRepository
    {
        private  List<Articles> list = new();

        public List<Articles> SelectByAvailability()
        {
            List<Articles> availableArticles = new();
            foreach (Articles article in list)
            {
                if (article.QteStock != 0)
                {
                    availableArticles.Add(article);
                }
            }
            return availableArticles;
        }
        public Articles? SelectByReference(string reference)
        {
            foreach (Articles article in list)
            {
                if (article.Reference.Equals(reference, StringComparison.OrdinalIgnoreCase))
                {
                    return article;
                }
            }
            return null;
        }
    }
}