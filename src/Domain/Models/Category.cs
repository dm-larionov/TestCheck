namespace Domain.Models
{
    public class Category
    {
        public Category()
        {
            Goods = new HashSet<Good>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Good> Goods { get; set; }
    }
}