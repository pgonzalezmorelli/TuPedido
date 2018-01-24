using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;

namespace TuPedido.Services
{
    public class UserService : IUserService
    {
        private readonly IRestClient requestProvider;
        private readonly IConfiguration configuration;
        private readonly IDropboxServiceClient dropboxService;
        private readonly IExcelHelper excelHelper;

        public UserService(IRestClient requestProvider, IConfiguration configuration, IDropboxServiceClient dropboxService, IExcelHelper excelHelper)
        {
            this.requestProvider = requestProvider;
            this.configuration = configuration;
            this.dropboxService = dropboxService;
            this.excelHelper = excelHelper;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var employees = await requestProvider.GetAsync<IEnumerable<Employee>>(configuration.Endpoints.GetUsersEndpoint);

            return employees
                .Where(u => !u.Deleted)
                .Select(u => new User { Name = u.Name })
                .OrderBy(u => u.Name)
                .ToList();
        }

        public async Task SaveUserAsync(User user)
        {
            var fileName = configuration.Dropbox.FileName;
            var bytes = await dropboxService.GetFileAsync(fileName);
            var stream = new MemoryStream(bytes);

            var modifiedBytes = excelHelper.SaveUser(stream, user);
            await dropboxService.UploadFileAsync(fileName, modifiedBytes);
        }
    }
}
