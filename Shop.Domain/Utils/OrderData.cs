using System;

namespace Shop.Domain.Utils
{
    public class OrderData
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}