namespace Shop.Domain
{
    public interface IUserService
    {
        ServiceStatus RegisterUser(UserModel newUser);
    }
}