using TuPedido.Exceptions;

namespace TuPedido.Helpers
{
    public class ValidationHelper
    {
        public static void Check(bool condition, string message)
        {
            if (!condition) throw new ValidationException(message);
        }

        public static void CheckRepository(bool condition, string message)
        {
            if (!condition) throw new RepositoryException(message);
        }
    }
}
