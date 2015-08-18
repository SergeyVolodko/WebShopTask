using Shop.Domain.Entities;
using Shop.Domain.Utils;

namespace Shop.Domain.Services
{
    public interface IUserService
    {
        ServiceStatus RegisterUser(User newUser);
        User LoginUser(string login, string password);
    }
}