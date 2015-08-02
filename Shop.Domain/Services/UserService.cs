using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Services
{
    public enum ServiceStatus
    {
        Ok,
        Conflict
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public ServiceStatus RegisterUser(User newUser)
        {
            if (!repository.UserExists(newUser.Login))
            {
                repository.CreateUser(newUser);
                return ServiceStatus.Ok;
            }
            return ServiceStatus.Conflict;
        }

        public User LoginUser(string login, string password)
        {
            if (repository.UserExists(login))
            {
                var loggedUser = repository.GetUserByLoginAndPassword(login, password);
                
                return loggedUser
                       ?? new NotAuthorizedUser();
            }
            
            return new NotAuthorizedUser();
        }
    }
}
