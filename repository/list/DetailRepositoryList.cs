using csharp.core.config;
using csharp.entities;
using csharp.core.interfaces;
using System.Linq;
using csharp.enums;

namespace csharp.repository.list
{

    public class DetailRepositoryList : RepositoryListImpl<Detail>,IDetailRepository{

        public DetailRepositoryList(){
        }
    }

}