using FluentAssertions;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class DtoMapperTests
    {
        [Theory]
        [ShopAutoData]
        public void mappings_from_entities_to_dtos(
            User user,
            Prdouct prdouct,
            Cart cart)
        {
            var userDto = new DtoMapper<UserDto>().MapFrom(user);
            var productDto = new DtoMapper<ProductDto>().MapFrom(prdouct);
            var cartDto = (CartDto)cart;

            userDto.ShouldBeEquivalentTo(user);
            productDto.ShouldBeEquivalentTo(prdouct, o => o.ExcludingMissingMembers());
            cartDto.ShouldBeEquivalentTo(cart, o => o.ExcludingMissingMembers());
        }

        [Theory]
        [ShopAutoData]
        public void mappings_from_dtos_to_entities(
            UserDto userDto)
        {
            var user = new DtoMapper<User>().MapFrom(userDto);
            var productDto = new ProductDto();
            var prdouct = (Prdouct)productDto;
            var cartDto = new CartDto();
            cartDto.Products.Add(productDto);
            var cart = (Cart)cartDto;

            user.ShouldBeEquivalentTo(userDto);
            prdouct.ShouldBeEquivalentTo(productDto, o => o.ExcludingMissingMembers());
            cart.ShouldBeEquivalentTo(cartDto, o => o.ExcludingMissingMembers());
        }

    }
}
