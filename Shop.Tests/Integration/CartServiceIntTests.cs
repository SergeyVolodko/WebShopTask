using FluentAssertions;
using Ninject;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Shop.Site;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class CartServiceIntTests
    {
        private readonly IKernel kernel;
        private readonly IArticleRepository repository;

        public CartServiceIntTests()
        {
            kernel = new Global(Consts.TEST_APP_DATA)
                .GetKernel();
            repository = kernel.Get<IArticleRepository>();
        }

        [Theory]
        [ShopAutoData]
        public void add_article_to_empty_cart_returns_new_cart_containing_article(
            Article article)
        {
            article = repository.Save(article);
            
            var sut = kernel.Get<ICartService>();

            var actual = sut.AddArticleToCart(null, article);

            actual
                .Should()
                .NotBeNull();
            actual
                .Articles[0]
                .ShouldBeEquivalentTo((ArticleDto)article);
        }
        
        //[Fact]
        //public void add_article_to_existing_cart_returns_cart_containing_article()
        //{
        //    var articles = new ArticleDataFactory()
        //        .CreateArticlesList(2);
        //    //repository.Save(articles);

        //    var sut = kernel.Get<ICartService>();

        //    var cart = sut.AddArticleToCart(null, articles[0]);

        //    var actual = sut.AddArticleToCart(cart.Id, articles[1]);

        //    actual.Id
        //        .Should()
        //        .Be(cart.Id);
        //    actual
        //        .Articles
        //        .Should()
        //        .Contain(articles[1]);
        //}
    }
}
