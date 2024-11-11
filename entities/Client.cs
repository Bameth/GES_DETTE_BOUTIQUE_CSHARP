namespace csharp.entities
{
    public class Client
    {
        private int id;
        private string surname;
        private string phone;
        private string addresse;
        private User user;
        private int? userId; // Store only the user ID
        private DateTime createdAt;
        private DateTime updatedAt;
        private static int count = 0;

        public int Id { get => id; set => id = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Addresse { get => addresse; set => addresse = value; }
        public User User { get => user; set => user = value; }
        public int? UserId { get => userId; set => userId = value; }
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
            return $"Client {id} : {surname} - {phone} - {addresse} - {user?.Name} - CreatedAt: {createdAt} - UpdatedAt: {updatedAt}";
        }
    }
}
