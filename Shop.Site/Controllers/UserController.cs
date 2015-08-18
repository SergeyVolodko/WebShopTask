using System.Net;
using System.Web.Http;
using Shop.Domain.Entities;
using Shop.Domain.Services;
using Shop.Domain.Utils;

namespace Shop.Site.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        
        public UserController(IUserService service)
        {
            userService = service;
        }

        public object Post(User newUser)
        {
            return
                userService.RegisterUser(newUser) == ServiceStatus.Conflict ?
                HttpStatusCode.Conflict
                : HttpStatusCode.Created;
        }

        [HttpGet]
        public string Get(string id)
        {
            return id;
        }
    }
}
