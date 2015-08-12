using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public Guid? Id { get; set; }
        public List<Prdouct> Products { get; set; }

        public Cart()
        {
            this.Products = new List<Prdouct>();
        }

        public virtual void AddProduct(Prdouct prdouct)
        {
            this.Products.Add(prdouct);
        }

        public static implicit operator CartDto(Cart cart)
        {
            var productDtos = cart.Products
                .Select(product => (ProductDto)product)
                .ToList();

            var cartDto = new DtoMapper<CartDto>()
                .MapFrom(cart);

            cartDto.Products = productDtos;
            foreach (var productDto in productDtos)
            {
                productDto.Carts.Add(cartDto);
            }

            return cartDto;
        }
    }
}