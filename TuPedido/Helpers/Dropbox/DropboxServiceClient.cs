using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using System.Threading.Tasks;

namespace TuPedido.Helpers
{
    public class DropboxServiceClient : IDropboxServiceClient
    {
        private readonly string accessToken;

        public DropboxServiceClient(IConfiguration configuration)
        {
            accessToken = configuration.Dropbox.AccessToken;
        }
        
        public async Task<byte[]> GetFileAsync(string filename)
        {
            using (DropboxClient client = new DropboxClient(accessToken))
            {
                IDownloadResponse<FileMetadata> resp = await client.Files.DownloadAsync(filename);
                return await resp.GetContentAsByteArrayAsync();
            }
        }
    }
}
