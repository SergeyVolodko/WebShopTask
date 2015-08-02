using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User newUser);
        bool UserExists(string login);
        User GetUserByLoginAndPassword(string login, string password);
    }
}