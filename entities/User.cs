using csharp.enums;

namespace csharp.entities
{
    public class User
    {
        private int id;
        private string name;
        private string login;
        private string password;
        private Role role;
        private TypeEtat typeEtat;
        private Client client;
        private DateTime createdAt;
        private DateTime updatedAt;
        private static int count = 0;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public Role Role { get => role; set => role = value; }
        public TypeEtat TypeEtat { get => typeEtat; set => typeEtat = value; }
        public Client Client { get => client; set => client = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }

        public User()
        {
            id = count++;
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"User {id} : {Name} - {Login} - {Role} - {TypeEtat} - CreatedAt: {createdAt} - UpdatedAt: {updatedAt}";
        }

    }
}
