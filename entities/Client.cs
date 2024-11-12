namespace csharp.entities
{
    public class Client
    {
        private int id;
        private string surname;
        private string phone;
        private string address;
        private User user;
        private int? userid;
        private DateTime createdAt;
        private DateTime updatedAt;
        private static int count = 0;

        public int Id { get => id; set => id = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public User? User { get => user; set => user = value ?? new User(); }
        public int? UserId { get => userid; set => userid = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }

        public Client()
        {
            id = count++;
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            if (user == null || user.Id == 0) // Ajout d'une vérification de l'ID de l'utilisateur
            {
                return $"Client {id} : {surname} - {phone} - {address} - Aucun utilisateur associé";
            }

            return $"Client {id} : {surname} - {phone} - {address} - {user.Name}"; // Affiche le nom de l'utilisateur
        }


    }
}
