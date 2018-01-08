using System;
using System.Threading.Tasks;
using TuPedido.Exceptions;

namespace TuPedido.Helpers
{
    public static class ErrorHelper
    {
        public static async Task TryExecuteAsync(Func<Task> execute, Func<Exception, Task> onError = null)
        {
            try
            {
                await execute();
            }
            catch (Exception ex)
            {
                if (onError != null)
                    await onError(ex);

                throw ex;
            }
        }

        public static async Task<T> TryExecuteAsync<T>(Func<Task<T>> execute, Func<Exception, Task<T>> onError = null)
        {
            try
            {
                return await execute();
            }
            catch (Exception ex)
            {
                if (onError!=null)
                    return await onError(ex);

                throw ex;
            }
        }

        public static Task<T> TryExecuteServiceAsync<T>(Func<Task<T>> action, Func<Exception, Task<T>> onError = null)
        {
            return TryExecuteAsync<T>(action,
                async ex =>
                {
                    if (onError != null) await onError(ex);
                    if (ex is ServiceErrorException) throw ex;
                    throw new ServiceException(ex.Message, ex);
                });
        }

        public static Task<T> TryExecuteRepositoryAsync<T>(Func<Task<T>> action, Func<Exception, Task<T>> onError = null)
        {
            return TryExecuteAsync<T>(action,
                async ex =>
                {
                    if (onError != null) await onError(ex);
                    throw new RepositoryException(ex.Message, ex);
                });
        }
    }
}
