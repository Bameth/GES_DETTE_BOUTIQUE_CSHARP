namespace csharp.core.interfaces
{
    public abstract class ViewImpl<T> : IView<T>
    {
        protected ViewImpl()
        {
        }

        protected static string Input()
        {
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
        public void Affiche(List<T> datas)
        {
            foreach (var data in datas)
            {
                Console.WriteLine(data);
            }
        }
        public abstract T Saisie();
    }
}
