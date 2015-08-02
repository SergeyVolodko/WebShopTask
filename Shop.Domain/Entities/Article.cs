using System;
using System.Xml.Serialization;

namespace Shop.Domain.Entities
{
    [Serializable]
    [XmlRoot("Article")]
    public class Article
    {
        [XmlAttribute]
        public string Name;
        public string Description;
        public double Price;
    }
}
