using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);

        Task AddUserAsync(User user);
    }
}
