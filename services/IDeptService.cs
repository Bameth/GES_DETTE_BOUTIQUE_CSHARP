using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;

namespace csharp.services
{
    public interface IDeptService : IService<Dept>
    {
        Dept FindById(int id);
        List<Dept> FindDebtsByClientId(int clientId);
        List<Dept> FindAllDeptNonSoldees();
        List<Dept> FindAllMyDeptNonSoldees(int clientId);
        List<Dept> FindAllMyDebts(int id);
        List<Dept> FindByEtat(TypeDette etat);
        public List<Dept> FindCanceledDebtsByClientId(int clientId);
    }
}
