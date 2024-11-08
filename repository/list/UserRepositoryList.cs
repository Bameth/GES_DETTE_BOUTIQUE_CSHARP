using csharp.core.config;
using csharp.entities;
using csharp.core.interfaces;
using System.Linq;
using csharp.enums;

namespace csharp.repository.list
{
    public class UserRepositoryList : RepositoryListImpl<User>, IUserRepository
    {
        private readonly List<User> list = new();

        public User SelectByName(string name)
        {
            return list.FirstOrDefault(user => string.Equals(user.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public User SelectByLogin(string login, string password)
        {
            return list.FirstOrDefault(user =>
                string.Equals(user.Login, login, StringComparison.OrdinalIgnoreCase) &&
                user.Password == password);
        }




        public List<User> SelectByEtat(TypeEtat etat)
        {
            return list.Where(user => user.TypeEtat == etat).ToList();
        }

        public List<User> SelectByRole(Role role)
        {
            return list.Where(user => user.Role == role).ToList();
        }
    }
}
