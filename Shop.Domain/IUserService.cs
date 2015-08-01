using Shop.Domain.Entities;

namespace Shop.Domain
{
    public interface IUserService
    {
        ServiceStatus RegisterUser(UserModel newUser);
        UserModel LoginUser(string login, string password);
    }
}