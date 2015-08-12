using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Repositories
{
    public class ProductNhibRepository: IProductRepository
    {
        private readonly ISession session;

        public ProductNhibRepository(ISession session)
        {
            this.session = session;
        }

        public List<Prdouct> GetAll()
        {
            var products = session.QueryOver<ProductDto>().List();
            return products
                .Select(a => (Prdouct)a)
                .ToList();
        }

        public List<Prdouct> GetTenProducts(int startIndex)
        {
            var products = session.QueryOver<ProductDto>().List();
            return products
                .Select(a => (Prdouct)a)
                .ToList()
                .GetRange(startIndex, 10);
        }

        public int GetProductsCount()
        {
            return session.QueryOver<ProductDto>().RowCount();
        }

        public void Save(List<Prdouct> products)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var product in products)
                {
                    session.Save((ProductDto)product);
                }
                
                transaction.Commit();
            }
        }

        public Prdouct Save(Prdouct prdouct)
        {
            var dto = (ProductDto)prdouct;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(dto);
                transaction.Commit();
            }
            return dto == null ? null
                   : (Prdouct)dto;
        }
    }
}
