using csharp.core.config;
using csharp.entities;
using csharp.enums;

namespace csharp.core.interfaces
{
public interface IDeptRepository : IRepository<Dept>{
    List<Dept> SelectByClientId(int clientId);
    List<Dept> SelectAllDeptNonSoldees();
    List<Dept> SelectAllMyDeptNonSoldees(int clientId);
    List<Dept> SelectAllMyDept(int clientId);
    List<Dept> SelectByEtat(TypeDette etat);
    List<Dept> SelectAllMyDeptAnnuler(int clientId);
}
}