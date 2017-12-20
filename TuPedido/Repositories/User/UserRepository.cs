using System;
using System.Threading.Tasks;
using TuPedido.Exceptions;
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
            return database.First();
        }

        public async Task AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            var result = await TryExecute(() => database.Insert(user));
            ValidationHelper.CheckRepository(result > 0, "Couldn't save user");
        }
        
        public async Task RemoveUser(User user)
        {
            var result = await TryExecute(() => database.Delete(user));
            ValidationHelper.CheckRepository(result > 0, "Couldn't remove user");
        }

        private async Task<T> TryExecute<T>(Func<Task<T>> execute, Func<Task<T>> onError = null)
        {
            try
            {
                return await execute();
            }
            catch (Exception ex)
            {
                await onError?.Invoke();
                throw new RepositoryException(ex.Message, ex);
            }
        }
    }
}
