using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
