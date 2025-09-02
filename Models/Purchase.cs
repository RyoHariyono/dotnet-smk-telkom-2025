using System;

namespace BookStore.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}