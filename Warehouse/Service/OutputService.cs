using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Model;

namespace Warehouse.Service
{
    internal class OutputService
    {
        public void ExportToExcel(DataGrid dataGrid, string filePath, string title, OrderedDictionary productFromOrder, DateTime firstDate, DateTime secondDate)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                if (excelPackage.Workbook.Worksheets.Any(x => x.Name == "Sheet1"))
                {
                    excelPackage.Workbook.Worksheets.Delete("Sheet1");
                }

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                string dateRange = $"Период: {firstDate.ToString("yyyy-MM-dd")} - {secondDate.ToString("yyyy-MM-dd")}";

                worksheet.Cells["A1:G1"].Merge = true;
                worksheet.Cells["A1:G1"].Value = title;
                worksheet.Cells["A1:G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:G2"].Merge = true;
                worksheet.Cells["A2:G2"].Value = dateRange;
                worksheet.Cells["A2:G2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A3:G3"].Merge = true;
                worksheet.Cells["A3:G3"].Value = "ОАО Гомельский Мясокомбинат";
                worksheet.Cells["A3:G3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A4:G4"].Merge = true;
                worksheet.Cells["A4:G4"].Value = "Адрес: Гомель, ул. Ильича, 2";
                worksheet.Cells["A4:G4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A5:G5"].Merge = true;

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    worksheet.Cells[6, i + 1].Value = dataGrid.Columns[i].Header;
                }

                int lastRow = 7;

                foreach (DataRowView rowView in dataGrid.Items)
                {
                    DataRow row = rowView.Row;

                    DateTime orderDate = (DateTime)row["order_date"];

                    if (orderDate >= firstDate && orderDate <= secondDate)
                    {
                        for (int col = 0; col < dataGrid.Columns.Count; col++)
                        {
                            var cellValue = row.ItemArray[col]?.ToString();
                            worksheet.Cells[lastRow, col + 1].Value = cellValue;
                        }

                        lastRow++;
                    }
                }

                lastRow = 7;

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

                try
                {
                    excelPackage.Save();
                } catch (Exception)
                {
                    MessageBox.Show("Для открытия отчёта закройте Excel!");
                }

            }

            Process.Start(filePath);
        }

    }
}