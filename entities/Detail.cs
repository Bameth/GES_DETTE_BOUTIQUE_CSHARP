namespace csharp.entities
{
    public class Detail
    {
        private int id;
        private int qte;
        private Articles articles;
        private Dept dept;
        private DateTime createdAt;
        private DateTime updatedAt;
        private static int count = 0;

        public int Id { get => id; set => id = value; }
        public int Qte { get => qte; set => qte = value; }
        public Articles Articles { get => articles; set => articles = value; }
        public Dept Dept { get => dept; set => dept = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }

        public Detail()
        {
            id = count++;
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Detail {id} : {qte} - {articles.Libelle} - {dept} - CreatedAt: {createdAt} - UpdatedAt: {updatedAt}";
        }
    }
}
