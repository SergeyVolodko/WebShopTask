using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public class ProductNhibRepository: IProductRepository
    {
        private readonly ISession session;

        public ProductNhibRepository(ISession session)
        {
            this.session = session;
        }

        public List<Product> GetAll()
        {
            var products = session.QueryOver<Product>().List();
            return products
                .Select(p => (Product)p)
                .ToList();
        }

        public List<Product> GetTenProducts(int startIndex)
        {
            var products = session.QueryOver<Product>().List();
            return products
                .Select(p => (Product)p)
                .ToList()
                .GetRange(startIndex, 10);
        }

        public int GetProductsCount()
        {
            return session.QueryOver<Product>().RowCount();
        }

        public void Save(List<Product> products)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var product in products)
                {
                    session.Save(product);
                }
                
                transaction.Commit();
            }
        }

        public Product Save(Product product)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(product);
                transaction.Commit();
            }
            return product;
        }
    }
}
