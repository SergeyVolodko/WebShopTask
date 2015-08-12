using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Dto
{
    public class CartDto
    {
        public virtual Guid? Id { get; set; }
        public virtual IList<ProductDto> Products { get; set; }

        public CartDto()
        {
            this.Products = new List<ProductDto>();
        }

        public virtual void AddProduct(ProductDto product)
        {
            product.Carts.Add(this);
            this.Products.Add(product);
        }

        public static implicit operator Cart(CartDto dto)
        {
            var products = dto.Products.Select(product => (Prdouct)product).ToList();

            var cart = new DtoMapper<Cart>()
                .MapFrom(dto);

            cart.Products = products;

            return cart;
        }
    }
}