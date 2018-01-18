using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TuPedido.Models;

namespace TuPedido.Helpers
{
    public class ExcelHelper : IExcelHelper
    {
        private readonly IConfiguration configuration;
        
        public ExcelHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Order> Parse(Stream stream)
        {
            using (var engine = new ExcelEngine())
            {
                IWorkbook workbook = null;

                try
                {
                    var application = engine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2013;

                    workbook = application.Workbooks.Open(stream, ExcelParseOptions.ParseWorksheetsOnDemand);
                    var sheet = workbook.Worksheets[0];
                    var rowsCount = sheet.Rows.Length;
                    
                    var data = (from row in Enumerable.Range(2, rowsCount - 1)
                                let estimatedMinutes = sheet.GetValueRowCol(row, 5)?.ToString()
                                let receivedDate = sheet.GetValueRowCol(row, 6)?.ToString()
                                let notifiedDate = sheet.GetValueRowCol(row, 7)?.ToString()
                                let deviceId = sheet.GetValueRowCol(row, 8)?.ToString()
                                select new Order
                                {
                                    Id = Guid.Parse(sheet.GetValueRowCol(row, 1).ToString()),
                                    Owner = sheet.GetValueRowCol(row, 2).ToString(),
                                    Service = sheet.GetValueRowCol(row, 3).ToString(),
                                    Date = DateTime.Parse(sheet.GetValueRowCol(row, 4).ToString()),
                                    EstimatedDelayMinutes = string.IsNullOrEmpty(estimatedMinutes) ? (int?)null : int.Parse(estimatedMinutes),
                                    ReceivedDate = string.IsNullOrEmpty(receivedDate) ? (DateTime?)null : DateTime.Parse(receivedDate),
                                    NotificationDate = string.IsNullOrEmpty(notifiedDate) ? (DateTime?)null : DateTime.Parse(notifiedDate),
                                    DeviceId = string.IsNullOrEmpty(deviceId) ? (Guid?)null : Guid.Parse(deviceId),
                                    DevicePlatform = sheet.GetValueRowCol(row, 9)?.ToString()
                                }).ToList();

                    return data;
                }
                finally
                {
                    workbook?.Close();
                }
            }
        }
    }
}
