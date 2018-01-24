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
        public IEnumerable<Order> GetOrders(Stream stream)
        {
            return ReadExcel(stream, workbook =>
            {
                var sheet = GetSheet<Order>(workbook);

                var data = sheet.Rows
                                .Skip(1)
                                .TakeWhile(r => !string.IsNullOrEmpty(sheet[r.Row, 1].Value))
                                .Select(usedRow => ReadOrder(sheet, usedRow.Row))
                                .ToList();

                return data;
            });
        }

        public Order GetOrder(Stream stream, Guid id)
        {
            return ReadExcel(stream, workbook =>
            {
                var sheet = GetSheet<Order>(workbook);

                var data = (from usedRow in sheet.Rows
                            where usedRow.Row > 1
                            where TryParse(sheet[usedRow.Row, 1].Value, Guid.Parse) == id
                            select ReadOrder(sheet, usedRow.Row))
                            .FirstOrDefault();

                return data;
            });
        }

        public byte[] SaveUser(Stream stream, User user)
        {
            return WriteExcel(stream, workbook =>
            {
                var sheet = GetSheet<User>(workbook);

                var existingUser = sheet.Rows.FirstOrDefault(r => sheet[r.Row, 1].Value.ToLowerInvariant() == user.Name.ToLowerInvariant());

                var rowToWrite = existingUser != null ? existingUser.Row : sheet.Rows.Count() + 1;
                WriteUser(sheet, rowToWrite, user);
            });
        }

        #region Helpers

        private T ReadExcel<T>(Stream stream, Func<IWorkbook, T> parser)
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

        private byte[] WriteExcel(Stream stream, Action<IWorkbook> transformation)
        {
            return ReadExcel(stream, workbook =>
            {
                transformation(workbook);
                return SaveChanges(stream, workbook);
            });
        }

        private byte[] SaveChanges(Stream stream, IWorkbook workbook)
        {
            var streamToSave = new MemoryStream();
            stream.CopyTo(streamToSave);
            workbook.SaveAs(streamToSave);

            return streamToSave.ToArray();
        }

        private Order ReadOrder(IWorksheet sheet, int row)
        {
            var comments = sheet[row, 6].Value;
            var devicePlatform = sheet[row, 10].FormulaStringValue;

            return new Order
            {
                Id = Guid.Parse(sheet[row, 1].Value),
                Owner = sheet[row, 2].Value,
                Service = sheet[row, 3].Value,
                Date = DateTime.Parse(sheet[row, 4].Value),
                EstimatedDelayMinutes = TryParse(sheet[row, 5].Value, int.Parse),
                Comments = string.IsNullOrEmpty(comments) ? null : comments,
                ReceivedDate = TryParse(sheet[row, 7].Value, DateTime.Parse),
                NotificationDate = TryParse(sheet[row, 8].Value, DateTime.Parse),
                DeviceId = TryParse(sheet[row, 9].FormulaStringValue, Guid.Parse),
                DevicePlatform = string.IsNullOrEmpty(devicePlatform) ? null : devicePlatform
            };
        }

        private void WriteUser(IWorksheet sheet, int row, User user)
        {
            sheet[row, 1].Value = user.Name;
            sheet[row, 2].Value = user.DeviceId.ToString();
            sheet[row, 3].Value = user.DevicePlatform;
        }

        private IWorksheet GetSheet<T>(IWorkbook workbook)
        {
            if (typeof(T) == typeof(Order))
            {
                return workbook.Worksheets[0];
            }
            else if (typeof(T) == typeof(User))
            {
                return workbook.Worksheets[1];
            }
            throw new InvalidOperationException();
        }

        private T? TryParse<T>(string value, Func<string, T> parser)
            where T : struct
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return parser(value);
        }

        #endregion Helpers
    }
}
