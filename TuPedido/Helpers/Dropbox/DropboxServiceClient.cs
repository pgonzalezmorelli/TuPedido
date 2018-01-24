using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using System.Threading.Tasks;
using System;
using System.IO;

namespace TuPedido.Helpers
{
    public class DropboxServiceClient : IDropboxServiceClient
    {
        #region Attributes & Properties

        private readonly string accessToken;
        private readonly IDates dates;

        #endregion Attributes & Properties

        public DropboxServiceClient(IConfiguration configuration, IDates dates)
        {
            this.accessToken = configuration.Dropbox.AccessToken;
            this.dates = dates;
        }

        public Task<byte[]> GetFileAsync(string filename)
        {
            return ErrorHelper.TryExecuteServiceAsync(async () =>
            {
                using (DropboxClient client = new DropboxClient(accessToken))
                {
                    IDownloadResponse<FileMetadata> resp = await client.Files.DownloadAsync(filename);
                    return await resp.GetContentAsByteArrayAsync();
                }
            });
        }

        public Task UploadFileAsync(string filename, byte[] fileContent)
        {
            return ErrorHelper.TryExecuteServiceAsync(async () =>
            {
                var commitInfo = new CommitInfo(filename, WriteMode.Overwrite.Instance, false, dates.Now);

                using (DropboxClient client = new DropboxClient(accessToken))
                {
                    await client.Files.UploadAsync(commitInfo, new MemoryStream(fileContent));
                    return true;
                }
            });
        }
    }
}
