using System;
using System.Net;
using System.Web;
using System.Web.Http;
using Shop.Domain;

namespace Shop.Site.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        
        public UserController(IUserService service)
        {
            userService = service;
        }

        public object Post(UserModel newUser)
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
