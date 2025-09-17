using System.Collections.Generic;

namespace BookStore.Models
{
    public class Customer : Base
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}