using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public required Guid Client { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


    }


    public class CartItem
    {
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
    }
}