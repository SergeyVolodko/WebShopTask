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
            Article article,
            Cart cart)
        {
            var userDto = new DtoMapper<UserDto>().MapFrom(user);
            var articleDto = new DtoMapper<ArticleDto>().MapFrom(article);
            var cartDto = (CartDto)cart;

            userDto.ShouldBeEquivalentTo(user);
            articleDto.ShouldBeEquivalentTo(article, o => o.ExcludingMissingMembers());
            cartDto.ShouldBeEquivalentTo(cart, o => o.ExcludingMissingMembers());
        }

        [Theory]
        [ShopAutoData]
        public void mappings_from_dtos_to_entities(
            UserDto userDto)
        {
            var user = new DtoMapper<User>().MapFrom(userDto);
            var articleDto = new ArticleDto();
            var article = (Article)articleDto;
            var cartDto = new CartDto();
            cartDto.Articles.Add(articleDto);
            var cart = (Cart)cartDto;

            user.ShouldBeEquivalentTo(userDto);
            article.ShouldBeEquivalentTo(articleDto, o => o.ExcludingMissingMembers());
            cart.ShouldBeEquivalentTo(cartDto, o => o.ExcludingMissingMembers());
        }

    }
}
