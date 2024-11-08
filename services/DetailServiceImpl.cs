using csharp.core.interfaces;
using csharp.entities;

namespace csharp.services
{
    public class DetailServiceImpl : IDetailService
    {
        private readonly IDetailRepository detailRepository;

        public DetailServiceImpl(IDetailRepository detailRepository)
        {
            this.detailRepository = detailRepository;
        }

        public int Count()
        {
            return detailRepository.Count();
        }

        public void Create(Detail objet)
        {
            detailRepository.Insert(objet);
        }

        public List<Detail> FindAll()
        {
            return detailRepository.SelectAll();

        }

        public Detail FindById(int id)
        {
            return detailRepository.SelectById(id);

        }

        public Detail GetBy(string objet)
        {
            throw new NotImplementedException();
        }

        public bool Update(Detail objet)
        {
            return detailRepository.Update(objet);

        }
    }
}
