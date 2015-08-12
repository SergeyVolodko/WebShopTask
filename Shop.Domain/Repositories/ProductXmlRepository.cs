using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public class ProductXmlRepository: IProductRepository
    {
        private readonly string xmlFile;

        public ProductXmlRepository(string xmlFile)
        {
            this.xmlFile = xmlFile;
        }

        public List<Prdouct> GetAll()
        {
            var result = new List<Prdouct>();

            if (File.Exists(xmlFile))
            {
                var serializer = new XmlSerializer(typeof(List<Prdouct>));

                using (var reader = new FileStream(xmlFile, FileMode.OpenOrCreate))
                {
                    result = (List<Prdouct>)serializer.Deserialize(reader);
                }
            }

            return result;
        }

        public List<Prdouct> GetTenProducts(int startIndex)
        {
            var products = GetAll();
            return products.GetRange(startIndex, 10);
        }

        public int GetProductsCount()
        {
            var products = GetAll();
            return products.Count;
        }

        public void Save(List<Prdouct> products)
        {
            var serializer = new XmlSerializer(typeof(List<Prdouct>));

            using (var writer = new FileStream(xmlFile, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(writer, products);
            }
        }

        public Prdouct Save(Prdouct prdouct)
        {
            throw new System.NotImplementedException();
        }
    }
}
