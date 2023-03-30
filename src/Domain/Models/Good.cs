namespace Domain.Models
{
    public class Good
    {
        public Good()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}