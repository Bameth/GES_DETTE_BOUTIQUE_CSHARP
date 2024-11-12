using csharp.core.interfaces;
using csharp.entities;

namespace csharp.services
{
    public class ArticleServiceImpl : IArticleService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleServiceImpl(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public int Count()
        {
            return articleRepository.Count();
        }

        public bool Create(Articles objet)
        {
            return articleRepository.Insert(objet);
        }

        public List<Articles> FindAll()
        {
           return articleRepository.SelectAll();
        }

        public List<Articles> FindAvailable()
        {
            return articleRepository.SelectByAvailability();
        }

        public Articles GetBy(string objet)
        {
            return articleRepository.SelectByReference(objet);
        }

        public bool Update(Articles objet)
        {
           articleRepository.Update(objet);
           return true;
        }
    }
}