using System.Collections.Generic;

namespace BookStore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}