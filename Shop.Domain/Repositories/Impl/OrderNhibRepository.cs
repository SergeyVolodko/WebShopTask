using NHibernate;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories.Impl
{
    public class OrderNhibRepository: IOrderRepository 
    {
        private readonly ISession session;

        public OrderNhibRepository(ISession session)
        {
            this.session = session;
        }

        public ShopOrder Save(ShopOrder order)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(order);
                transaction.Commit();
            }
            return order;
        }
    }
}
