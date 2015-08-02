
using Shop.Site;

namespace Shop.Tests.Integration
{
    public static class Consts
    {
        public static string TEST_DB_PATH = @"E:\Projects\WebShopTask\Shop.Site\App_Data\tests.db";
        public static string TEST_ARTICLE_XML = @"E:\Projects\WebShopTask\Shop.Site\App_Data\test_articles.xml";

        public static AppTestingData TEST_APP_DATA = new AppTestingData
        {
            ArticlesXmlPath = TEST_ARTICLE_XML,
            DataBasePath = TEST_DB_PATH
        };
    }
}
