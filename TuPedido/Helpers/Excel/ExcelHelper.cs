using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syncfusion.XlsIO;
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

        public IEnumerable<Order> GetOrders(Stream stream)
        {
            return Parse(stream, workbook =>
            {
                var sheet = workbook.Worksheets[0];

                var data = sheet.Rows
                                .Skip(1)
                                .TakeWhile(r => !string.IsNullOrEmpty(sheet[r.Row, 1].Value))
                                .Select(usedRow => RowToOrder(sheet, usedRow.Row))
                                .ToList();

                return data;
            });
        }

        public Order GetOrder(Stream stream, Guid id)
        {
            return Parse(stream, workbook => 
            { 
                var sheet = workbook.Worksheets[0];

                var data = (from usedRow in sheet.Rows
                            where usedRow.Row > 1
                            where TryParse(sheet[usedRow.Row, 1].Value, Guid.Parse) == id
                            select RowToOrder(sheet, usedRow.Row))
                            .FirstOrDefault();

                return data;
            });
        }

        private T Parse<T>(Stream stream, Func<IWorkbook, T> parser)
        {
            using (var engine = new ExcelEngine())
            {
                IWorkbook workbook = null;

                try
                {
                    var application = engine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2013;

                    workbook = application.Workbooks.Open(stream, ExcelParseOptions.ParseWorksheetsOnDemand);
                    return parser(workbook);
                }
                finally
                {
                    workbook?.Close();
                }
            }
        }

        private Order RowToOrder(IWorksheet sheet, int row)
        {
            return new Order
            {
                Id = Guid.Parse(sheet[row, 1].Value),
                Owner = sheet[row, 2].Value,
                Service = sheet[row, 3].Value,
                Date = DateTime.Parse(sheet[row, 4].Value),
                EstimatedDelayMinutes = TryParse(sheet[row, 5].Value, int.Parse),
                ReceivedDate = TryParse(sheet[row, 6].Value, DateTime.Parse),
                NotificationDate = TryParse(sheet[row, 7].Value, DateTime.Parse),
                DeviceId = TryParse(sheet[row, 8].FormulaStringValue, Guid.Parse),
                DevicePlatform = sheet[row, 9].FormulaStringValue
            };
        }

        private T? TryParse<T>(string value, Func<string, T> parser)
            where T : struct
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return parser(value);
        }
    }
}
