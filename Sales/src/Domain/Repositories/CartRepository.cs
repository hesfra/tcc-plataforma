using Domain.Entities;
using System;

namespace Domain.Repositories
{
    public interface ICartRepository
    {
        void AddCart(Cart cart);
        Cart GetCartById(Guid id);
        IEnumerable<Cart> GetAllCarts();

        IEnumerable<Cart> GetCartsByClientId(Guid clientId);

        void AddProductToCart(Guid cartId, CartItem item);

        void RemoveProductFromCart(Guid cartId, Guid productId);

        void ClearCart(Guid cartId);


        void DeleteCart(Guid id);
    }
}