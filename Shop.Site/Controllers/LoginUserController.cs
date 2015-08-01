using System.Web.Http;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Site.Models;

namespace Shop.Site.Controllers
{
    public class LoginUserController : ApiController
    {
        private readonly IUserService userService;

        public LoginUserController(IUserService service)
        {
            userService = service;
        }
        public UserModel Post(LoginData loginData)
        {
            return userService.LoginUser(loginData.Login, loginData.Password);
        }
    }
}
