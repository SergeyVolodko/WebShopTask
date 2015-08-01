using Shop.Domain.Entities;

namespace Shop.Domain
{
    public interface IUserRepository
    {
        void CreateUser(UserModel newUser);
        bool UserExists(string login);
        UserModel GetUserByLoginAndPassword(string login, string password);
    }
}