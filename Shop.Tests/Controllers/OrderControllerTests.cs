using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Services;
using Shop.Domain.Utils;
using Shop.Site.Controllers;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Theory]
        [ShopControllerAutoData]
        public void post_invokes_service_create_order(
            [Frozen] IOrderService service,
            OrderController sut,
            OrderData data)
        {
            sut.Post(data);

            service.Received()
                .CreateOrder(data);
        }
    }
}
