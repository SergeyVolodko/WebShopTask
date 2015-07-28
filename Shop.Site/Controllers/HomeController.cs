using System.Web.Mvc;
using Shop.Domain;

namespace Shop.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}