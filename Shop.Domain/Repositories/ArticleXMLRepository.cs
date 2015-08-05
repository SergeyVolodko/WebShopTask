using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public class ArticleXMLRepository: IArticleRepository
    {
        private readonly string xmlFile;

        public ArticleXMLRepository(string xmlFile)
        {
            this.xmlFile = xmlFile;
        }

        public List<Article> GetAll()
        {
            var result = new List<Article>();

            if (File.Exists(xmlFile))
            {
                var serializer = new XmlSerializer(typeof(List<Article>));

                using (var reader = new FileStream(xmlFile, FileMode.OpenOrCreate))
                {
                    result = (List<Article>)serializer.Deserialize(reader);
                }
            }

            return result;
        }

        public List<Article> GetTenArticles(int startIndex)
        {
            var articles = GetAll();
            return articles.GetRange(startIndex, 10);
        }

        public int GetArticlesCount()
        {
            var articles = GetAll();
            return articles.Count;
        }

        public void Save(List<Article> articles)
        {
            var serializer = new XmlSerializer(typeof(List<Article>));

            using (var writer = new FileStream(xmlFile, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(writer, articles);
            }
        }
    }
}
