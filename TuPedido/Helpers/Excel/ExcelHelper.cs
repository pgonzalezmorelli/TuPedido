using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                try
                {
                    var application = engine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2013;

                    var workbook = application.Workbooks.Open(stream, ExcelParseOptions.ParseWorksheetsOnDemand);
                    var sheet = workbook.Worksheets[0];
                    var rowsCount = sheet.Rows.Length;

                    var data = (from row in Enumerable.Range(2, rowsCount - 1)
                           let estimatedMinutes = sheet.GetValueRowCol(row, 5)?.ToString()
                           let receivedDate = sheet.GetValueRowCol(row, 6)?.ToString()
                           let notifiedDate = sheet.GetValueRowCol(row, 7)?.ToString()
                           select new Order
                           {
                               Owner = sheet.GetValueRowCol(row, 2).ToString(),
                               Service = sheet.GetValueRowCol(row, 3).ToString(),
                               Date = DateTime.ParseExact(sheet.GetValueRowCol(row, 4).ToString(), configuration.Dropbox.DateTimeFormat, CultureInfo.CurrentUICulture),
                               EstimatedDelayMinutes = string.IsNullOrEmpty(estimatedMinutes) ? (int?)null : int.Parse(estimatedMinutes),
                               ReceivedDate = string.IsNullOrEmpty(receivedDate) ? (DateTime?)null : DateTime.ParseExact(receivedDate, configuration.Dropbox.DateTimeFormat, CultureInfo.CurrentUICulture),
                               NotificationDate = string.IsNullOrEmpty(notifiedDate) ? (DateTime?)null : DateTime.ParseExact(notifiedDate, configuration.Dropbox.DateTimeFormat, CultureInfo.CurrentUICulture),
                               DeviceId = sheet.GetValueRowCol(row, 8)?.ToString(),
                           }).ToList();

                    workbook.Close();

                    return data;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw ex;
                }
            }
        }
    }
}
