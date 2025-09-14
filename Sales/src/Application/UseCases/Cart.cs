using Domain.Entities;
using Domain.Repositories;
using System;


namespace Application.UseCases.Carts
{
    public class CreateCartHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public Cart Handle(CreateCartRequest request)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                Client = request.Client,
                Items = request.Items,
                CreatedAt = DateTime.UtcNow
            };
            _cartRepository.AddCart(cart);
            return cart;
        }
    }

    public record CreateCartRequest(Guid Client, List<CartItem> Items);

    public class DeleteCartHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public void Handle(Guid id)
        {
            var cart = _cartRepository.GetCartById(id) ?? throw new Exception("Cart not found");
            _cartRepository.DeleteCart(id);
        }
    }

    public class GetCartByIdHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public Cart Handle(Guid id)
        {
            var cart = _cartRepository.GetCartById(id) ?? throw new Exception("Cart not found");
            return cart;
        }
    }

    public class GetAllCartsHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public IEnumerable<Cart> Handle()
        {
            return _cartRepository.GetAllCarts();
        }
    }

    public class AddProductToCartHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public void Handle(Guid cartId, CartItem item)
        {
            var cart = _cartRepository.GetCartById(cartId) ?? throw new Exception("Cart not found");
            _cartRepository.AddProductToCart(cartId, item);
        }
    }

    public class RemoveProductFromCartHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public void Handle(Guid cartId, Guid productId)
        {
            var cart = _cartRepository.GetCartById(cartId) ?? throw new Exception("Cart not found");
            _cartRepository.RemoveProductFromCart(cartId, productId);
        }
    }

    public class ClearCartHandler(ICartRepository cartRepository)
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public void Handle(Guid cartId)
        {
            var cart = _cartRepository.GetCartById(cartId) ?? throw new Exception("Cart not found");
            _cartRepository.ClearCart(cartId);
        }
    }
}