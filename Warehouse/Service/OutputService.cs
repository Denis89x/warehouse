using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Warehouse.Model;

namespace Warehouse.Service
{
    internal class OutputService
    {
        public void ExportToExcel(DataGrid dataGrid, string filePath, string title, OrderedDictionary productFromOrder)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                if (excelPackage.Workbook.Worksheets.Any(x => x.Name == "Sheet1"))
                {
                    excelPackage.Workbook.Worksheets.Delete("Sheet1");
                }

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                worksheet.Cells["A1:G1"].Merge = true;
                worksheet.Cells["A1:G1"].Value = title;
                worksheet.Cells["A1:G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    worksheet.Cells[2, i + 1].Value = dataGrid.Columns[i].Header;
                }

                for (int row = 0; row < dataGrid.Items.Count; row++)
                {
                    for (int col = 0; col < dataGrid.Columns.Count; col++)
                    {
                        var cellValue = (dataGrid.Items[row] as DataRowView)?.Row.ItemArray[col]?.ToString();
                        worksheet.Cells[row + 3, col + 1].Value = cellValue;
                    }
                }

                int lastRow = 3;
                foreach (DictionaryEntry entry in productFromOrder)
                {
                    long orderId = (long)entry.Key;
                    List<Product> products = (List<Product>)entry.Value;

                    var productsString = string.Join(", ", products.Select(p => p.title));

                    worksheet.Cells[lastRow, 7].Value = productsString;

                    lastRow++;
                }

                worksheet.Cells.AutoFitColumns();

                var usedRange = worksheet.Cells[worksheet.Dimension.Address];
                var border = usedRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                excelPackage.Save();
            }

            Process.Start(filePath);
        }

    }
}
