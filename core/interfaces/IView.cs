namespace csharp.core.interfaces
{
    public interface IView<T>
    {
        T Saisie();
        void Affiche(List<T> datas);
    }
}