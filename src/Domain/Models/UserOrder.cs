namespace Domain.Models
{
    public class UserOrder
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual User OrderNavigation { get; set; } = null!;
    }
}