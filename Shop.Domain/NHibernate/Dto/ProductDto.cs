using System;
using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Dto
{
   public class ProductDto
    {
        public virtual Guid? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }

        public virtual IList<CartDto> Carts { get; set; }

        public ProductDto()
        {
            this.Carts = new List<CartDto>();
        }

        public static implicit operator Prdouct(ProductDto dto)
        {
            return new DtoMapper<Prdouct>().MapFrom(dto);
        }
    }
}
