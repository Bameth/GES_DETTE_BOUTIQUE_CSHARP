namespace csharp.core.config
{
    public interface IRepository<T>
    {
        Boolean Insert(T objet);
        Boolean Update(T objet);
        Boolean Delete(int id);
        List<T> SelectAll();
        T SelectById(int id);
        int Count();
    }
}