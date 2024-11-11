namespace csharp.entities
{
    public class Articles
    {
        private int id;
        private string reference;
        private string libelle;
        private double prix;
        private int qteStock;
        private DateTime createdAt;
        private DateTime updatedAt;
        private static int count = 0;

        public int Id { get => id; set => id = value; }
        public String Reference { get => reference; set => reference = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public double Prix { get => prix; set => prix = value; }
        public int QteStock { get => qteStock; set => qteStock = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }

        public Articles()
        {
            id = count++;
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
            GenerateReference();
        }

        public void GenerateReference()
        {
            reference = GenerateNumero(id, "ART");
        }

        public static string GenerateNumero(int nbre, string format)
        {
            int size = nbre.ToString().Length;
            return format + new string('0', (4 - size) < 0 ? 0 : 4 - size) + nbre;
        }

        public override string ToString()
        {
            return $"Article {id} : {reference} - {libelle} - {prix} - {qteStock} - CreatedAt: {createdAt} - UpdatedAt: {updatedAt}";
        }
    }
}
