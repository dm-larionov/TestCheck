namespace Domain.Models
{
    public class Order
    {
        public Order()
        {
            UserOrders = new HashSet<UserOrder>();
            Goods = new HashSet<Good>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<UserOrder> UserOrders { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}