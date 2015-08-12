using System;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Entities
{
    public class Prdouct
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public static implicit operator ProductDto(Prdouct prdouct)
        {
            return new DtoMapper<ProductDto>()
                .MapFrom(prdouct);
        }
    }
}
