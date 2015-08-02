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
    }
}
