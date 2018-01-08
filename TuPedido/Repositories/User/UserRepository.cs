using System;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;

namespace TuPedido.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDatabase<User> database;

        public UserRepository(IDatabase<User> db)
        {
            database = db;
        }

        public Task<User> GetCurrentUser()
        {
            return ErrorHelper.TryExecuteRepositoryAsync(
                () => database.First(),
                _ => Task.FromResult((User)null)
            );
        }

        public async Task AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            var result = await ErrorHelper.TryExecuteRepositoryAsync(() => database.Insert(user));
            ValidationHelper.CheckRepository(result > 0, "Couldn't save user");
        }
        
        public async Task RemoveUser(User user)
        {
            var result = await ErrorHelper.TryExecuteRepositoryAsync(() => database.Delete(user));
            ValidationHelper.CheckRepository(result > 0, "Couldn't remove user");
        }
    }
}
