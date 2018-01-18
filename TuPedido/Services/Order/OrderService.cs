using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;

namespace TuPedido.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDropboxServiceClient dropboxService;
        private readonly IExcelHelper excelHelper;
        private readonly IConfiguration configuration;

        public OrderService(IDropboxServiceClient dropboxService, IExcelHelper excelHelper, IConfiguration configuration)
        {
            this.dropboxService = dropboxService;
            this.excelHelper = excelHelper;
            this.configuration = configuration;
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return (await GetOrdersAsync()).FirstOrDefault(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var bytes = await dropboxService.GetFileAsync(configuration.Dropbox.FileName);
            return excelHelper.Parse(new MemoryStream(bytes));
        }

        public Task<Order> SaveOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
