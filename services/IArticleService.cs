namespace csharp.services
{
    using csharp.core.interfaces;
    using csharp.entities;
    public interface IArticleService : IService<Articles>
    {
        List<Articles> FindAvailable();
    }
}