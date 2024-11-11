using csharp.core.config;
using csharp.entities;
using csharp.enums;

namespace csharp.core.interfaces
{

public interface IUserRepository : IRepository<User>{
    User? SelectByName(string name);
    List<User> SelectByEtat(TypeEtat etat);
    List<User> SelectByRole(Role role);
    User? SelectByLogin(string login, string password);
}
}