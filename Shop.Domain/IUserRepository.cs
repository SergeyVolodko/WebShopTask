namespace Shop.Domain
{
    public interface IUserRepository
    {
        void CreateUser(UserModel newUser);
        bool UserExists(string login);
    }
}