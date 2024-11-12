namespace csharp.core.interfaces
{

    public interface IService<T>
    {
        bool Create(T objet);
        List<T> FindAll();
        Boolean Update(T objet);
        T? GetBy(String objet);
        int Count();
    }
}