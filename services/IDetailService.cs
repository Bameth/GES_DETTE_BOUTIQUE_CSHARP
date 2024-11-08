using csharp.entities;
using csharp.core.interfaces;

namespace csharp.services
{
    public interface IDetailService : IService<Detail> {
        Detail FindById(int id);
    }
}
