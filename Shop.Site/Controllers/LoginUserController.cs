using System.Web.Http;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Services;
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

        public User Post(LoginData loginData)
        {
            return userService.LoginUser(loginData.Login, loginData.Password);
        }
    }
}
