using csharp.core.config;
using csharp.entities;

namespace csharp.core.interfaces
{

    public interface IArticleRepository : IRepository<Articles>
    {
        List<Articles> SelectByAvailability();
        Articles SelectByReference(String reference);
    }
}