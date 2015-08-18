using System;
using Shop.Domain.Entities;
using Shop.Domain.Utils;

namespace Shop.Domain.Services
{
    public interface ICartService
    {
        Cart AddProductToCart(Guid? cartId, Guid productId);
        double GetSubtotal(Guid cartId);
        TotalSummary GetTotal(Guid cartId);
    }
}
