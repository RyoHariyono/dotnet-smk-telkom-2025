using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class Book : Base
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        /* ------------------------------- Relational ------------------------------- */
        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}