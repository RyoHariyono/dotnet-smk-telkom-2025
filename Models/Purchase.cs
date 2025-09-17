using System;

namespace BookStore.Models
{
    public class Purchase : Base
    {

        public Guid BookId { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer Customer { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}