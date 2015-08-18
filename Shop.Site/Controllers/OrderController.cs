using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Services;
using Shop.Domain.Utils;

namespace Shop.Site.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }

        public ShopOrder Post(OrderData data)
        {
            return service.CreateOrder(data);
        }
    }
}