using System;
using NHibernate;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories.Impl
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
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(cart);
                transaction.Commit();
            }
            return cart;
        }

        public Cart GetCartById(Guid cartId)
        {
            return session.QueryOver<Cart>()
                .Where(c => c.Id == cartId)
                .SingleOrDefault();
        }
    }
}
