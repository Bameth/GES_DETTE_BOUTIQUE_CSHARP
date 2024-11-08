namespace csharp.core.config
{
    public class RepositoryListImpl<T> : IRepository<T> where T : class
    {
        private List<T> list = new List<T>();

        public int Count()
        {
            return list.Count;
        }

        public bool Delete(int id)
        {
            var item = SelectById(id);
            if (item != null)
            {
                list.Remove(item);
                return true;
            }
            return false;
        }

        public bool Insert(T objet)
        {
            list.Add(objet);
            return true;
        }

        public List<T> SelectAll()
        {
            return list;
        }

        public T SelectById(int id)
        {
            return list.FirstOrDefault(item =>
            {
                var property = typeof(T).GetProperty("Id");
                return property != null && (int)property.GetValue(item) == id;
            });
        }

        public bool Update(T objet)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null) return false;

            int id = (int)property.GetValue(objet);
            var existingItem = SelectById(id);
            if (existingItem != null)
            {
                list.Remove(existingItem);
                list.Add(objet);
                return true;
            }
            return false;
        }
    }
}