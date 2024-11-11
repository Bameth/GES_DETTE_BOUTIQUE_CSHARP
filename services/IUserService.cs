using csharp.entities;
using csharp.core.interfaces;
using csharp.enums;

namespace csharp.services
{
    public interface IUserService : IService<User>
    {
        List<User> FindByEtat(TypeEtat etat);

        List<User> FindByRole(Role role);

        User? FindByLogin(string login, string password);

    }
}