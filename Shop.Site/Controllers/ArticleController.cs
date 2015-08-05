using System.Collections.Generic;
using System.Web.Http;
using Shop.Domain.Entities;
using Shop.Domain.Services;

namespace Shop.Site.Controllers
{
    public class ArticleController : ApiController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService service)
        {
            articleService = service;
        }

        public List<Article> Get()
        {
            return articleService.GetAllArticles();
        }

        public List<Article> GetPageArticles(int pageNumber)
        {
            // TODO: isn't it a mixing of responisbilities if use service.getPageArticcles ?
            var startIndex = pageNumber * 10 - 10;
            return articleService.GetTenArticlesFromIndex(startIndex);
        }
    }
}
