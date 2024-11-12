using csharp.core.interfaces;
using csharp.entities;
using csharp.enums;
namespace csharp.services
{
    public class DeptServiceImpl : IDeptService
    {
        private readonly IDeptRepository deptRepository;

        public DeptServiceImpl(IDeptRepository deptRepository)
        {
            this.deptRepository = deptRepository;
        }

        public int Count()
        {
            return deptRepository.Count();
        }

        public bool Create(Dept objet)
        {
            return deptRepository.Insert(objet);
        }

        public List<Dept> FindAll()
        {
            return deptRepository.SelectAll();
        }

        public List<Dept> FindAllDeptNonSoldees()
        {
            return deptRepository.SelectAllDeptNonSoldees();
        }

        public List<Dept> FindAllMyDebts(int id)
        {
            return deptRepository.SelectAllMyDept(id);
        }

        public List<Dept> FindAllMyDeptNonSoldees(int clientId)
        {
            return deptRepository.SelectAllMyDeptNonSoldees(clientId);
        }

        public List<Dept> FindByEtat(TypeDette etat)
        {
            return deptRepository.SelectByEtat(etat);
        }

        public Dept FindById(int id)
        {
            return deptRepository.SelectById(id);
        }

        public List<Dept> FindCanceledDebtsByClientId(int clientId)
        {
            return deptRepository.SelectAllMyDeptAnnuler(clientId);
        }

        public List<Dept> FindDebtsByClientId(int clientId)
        {
            return deptRepository.SelectByClientId(clientId);
        }

        public Dept GetBy(string objet)
        {
            throw new NotImplementedException();
        }

        public bool Update(Dept objet)
        {
            return deptRepository.Update(objet);
        }
    }
}