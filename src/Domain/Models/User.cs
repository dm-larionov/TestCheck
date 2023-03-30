namespace Domain.Models
{
    public class User
    {
        public User()
        {
            UserOrders = new HashSet<UserOrder>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Middlename { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<UserOrder> UserOrders { get; set; }
    }
}