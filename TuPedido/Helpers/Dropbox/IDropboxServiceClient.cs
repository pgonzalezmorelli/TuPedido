using System.IO;
using System.Threading.Tasks;

namespace TuPedido.Helpers
{
    public interface IDropboxServiceClient
    {
        Task<byte[]> GetFileAsync(string filename);

        Task UploadFileAsync(string filename, byte[] fileContent);
    }
}
