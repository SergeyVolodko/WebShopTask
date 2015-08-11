using System;
using NHibernate;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Repositories
{
    public class CartNHibRepository: ICartRepository
    {
        private readonly ISession session;

        public CartNHibRepository(ISession session)
        {
            this.session = session;
        }

        public Cart Save(Cart cart)
        {
            var dto = (CartDto) cart;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(dto);
                transaction.Commit();
            }
            return dto == null ? null 
                   : (Cart)dto;
        }

        public Cart GetCartById(Guid cartId)
        {
            var dto = session.QueryOver<CartDto>()
                .Where(c => c.Id == cartId)
                .SingleOrDefault();

            return dto == null ? null
                    : (Cart)dto;
        }
    }
}
