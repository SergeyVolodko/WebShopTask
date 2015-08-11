using System;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class CartServiceTests
    {
        [Theory]
        [ShopAutoData]
        public void add_article_to_empty_cart_invoke_factory_create_cart(
            [Frozen] ICartFactory factory,
            CartService sut)
        {
            var article = new ArticleDataFactory()
                .CreateArticle();

            sut.AddArticleToCart(null, article);

            factory
                .Received()
                .CreateCart(article);
        }
        
        [Theory]
        [ShopAutoData]
        public void add_article_to_empty_cart_invoke_repo_save(
            [Frozen] ICartRepository repo,
            [Frozen] ICartFactory factory,
            CartService sut)
        {
            var article = new ArticleDataFactory()
                .CreateArticle();

            var cart = new CartDataBuilder()
                .WithEmptyId()
                .WithArticle(article)
                .Build();

            factory.CreateCart(article)
                .Returns(cart);
            
            sut.AddArticleToCart(null, article);

            repo.Received()
                .Save(cart);
        }

        [Theory]
        [ShopAutoData]
        public void add_article_to_nonempty_cart_not_invoke_factory_create_cart(
            [Frozen] ICartFactory factory,
            [Frozen] ICartRepository repo,
            CartService sut,
            Guid cartId,
            Article article)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .WithArticle(article)
                .Build();

            repo.GetCartById(cartId)
                .Returns(cart);

            sut.AddArticleToCart(cartId, Arg.Any<Article>());

            factory.DidNotReceive()
                .CreateCart(Arg.Any<Article>());
        }

        [Theory]
        [ShopAutoData]
        public void add_article_to_nonempty_cart_invoke_repo_get_by_id(
            [Frozen] ICartRepository repo,
            CartService sut,
            Article article,
            Guid cartId)
        {
            sut.AddArticleToCart(cartId, article);

            repo.Received()
                .GetCartById(cartId);
        }

        [Theory]
        [ShopAutoData]
        public void add_article_to_existung_cart_invoke_repo_save(
            [Frozen] ICartRepository repo,
            CartService sut,
            Article article,
            Guid cartId)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .Build();

            repo.GetCartById(cartId)
                .Returns(cart);

            sut.AddArticleToCart(cartId, article);

            repo.Received()
                .Save(cart);
        }
    }
}
