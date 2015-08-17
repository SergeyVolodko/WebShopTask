using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Theory]
        [ShopControllerAutoData]
        public void post_invokes_service_create_order()
        {

        }
    }
}
