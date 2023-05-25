using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class OrderItem
    {
        [Required]
        public int ID { get; set; }
        public int? Quantity { get; set; }
        public double? DiscountPrice { get; set; }
        public int? OrderID { get; set; }
    }
}
