using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class InMemoryCartRepository : ICartRepository
    {
        private readonly List<Cart> _carts = new();

        public void AddCart(Cart cart)
        {
            _carts.Add(cart);
        }

        public void DeleteCart(Guid id)
        {
            var cart = _carts.FirstOrDefault(c => c.Id == id);
            if (cart != null)
            {
                _carts.Remove(cart);
            }
        }

        public Cart GetCartById(Guid id)
        {
            return _carts.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _carts;
        }

        public IEnumerable<Cart> GetCartsByClientId(Guid id)
        {
            return _carts;
        }

        public void AddProductToCart(Guid cartId, CartItem item)
        {
            var cart = _carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            cart.Items.Add(item);
        }

        public void RemoveProductFromCart(Guid cartId, Guid productId)
        {
            var cart = _carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                cart.Items.Remove(item);
        }

        public void ClearCart(Guid cartId)
        {
            var cart = _carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            cart.Items.Clear();
        }
    }
}
