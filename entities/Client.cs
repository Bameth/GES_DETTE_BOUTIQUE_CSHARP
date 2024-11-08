namespace csharp.entities
{
public class Client {
    private int id;

    private string surname;

    private string phone;

    private string address;

    private User user;
    private static int count = 0;

    public Client() {
        id = count++;
    }
            public int Id { get => id; set => id = value; }
            public string Surname { get => surname; set => surname = value; }
            public string Phone { get => phone; set => phone = value; }
            public string Address { get => address; set => address = value; }
            public User User { get => user; set => user = value; }



    public override string ToString()
        {
            return $"Client {id} : {surname} - {phone} - {address} - {user?.Name}";
        }
}
}