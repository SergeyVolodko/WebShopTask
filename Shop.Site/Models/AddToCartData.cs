using System;
using Shop.Domain.Entities;

namespace Shop.Site.Models
{
    public class AddToCartData
    {
        public Guid? CartId;
        public Article Article;
    }
}