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
                if (excelPackage.Workbook.Worksheets.Any(x => x.Name == "Отчёт о движении"))
                {
                    excelPackage.Workbook.Worksheets.Delete("Отчёт о движении");
                }

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Отчёт о движении");
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
                worksheet.Cells["A4:G4"].Value = "Главный склад";
                worksheet.Cells["A4:G4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A5:G5"].Merge = true;
                worksheet.Cells["A5:G5"].Value = "Адрес: Гомель, ул. Ильича, 2";
                worksheet.Cells["A5:G5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A6:G6"].Merge = true;

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    worksheet.Cells[7, i + 1].Value = dataGrid.Columns[i].Header;
                }

                int lastRow = 8;

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

                lastRow = 8;

                foreach (DictionaryEntry entry in productFromOrder)
                {
                    long orderId = (long)entry.Key;
                    List<Product> products = (List<Product>)entry.Value;

                    var productsString = string.Join(", ", products.Select(p => p.title));

                    worksheet.Cells[lastRow, 7].Value = productsString;

                    lastRow++;
                }

                worksheet.Cells.AutoFitColumns();

                var tableRange = worksheet.Cells[7, 1, lastRow - 1, dataGrid.Columns.Count];
                var border = tableRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                lastRow++;
                worksheet.Cells[lastRow, 1].Value = "Составил: _______________";

                try
                {
                    excelPackage.Save();
                }
                catch (Exception)
                {
                    MessageBox.Show("Для открытия отчёта закройте Excel!");
                    return;
                }
            }

            Process.Start(filePath);
        }

        public void ExcelFilter(DataGrid dataGrid, string filePath, string title, OrderedDictionary productFromOrder, DateTime firstDate, DateTime secondDate, String ExType)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                if (excelPackage.Workbook.Worksheets.Any(x => x.Name.Equals(ExType)))
                {
                    excelPackage.Workbook.Worksheets.Delete(ExType);
                }

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(ExType);
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
                worksheet.Cells["A4:G4"].Value = "Главный склад";
                worksheet.Cells["A4:G4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A5:G5"].Merge = true;
                worksheet.Cells["A5:G5"].Value = "Адрес: Гомель, ул. Ильича, 2";
                worksheet.Cells["A5:G5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A6:G6"].Merge = true;

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    worksheet.Cells[7, i + 1].Value = dataGrid.Columns[i].Header;
                }

                int lastRow = 8;

                foreach (DataRowView rowView in dataGrid.Items)
                {
                    DataRow row = rowView.Row;

                    DateTime orderDate = (DateTime)row["order_date"];
                    string type = (string)row["order_type"];

                    if (orderDate >= firstDate && orderDate <= secondDate)
                    {
                        if (type.Equals(ExType))
                        {
                            for (int col = 0; col < dataGrid.Columns.Count; col++)
                            {
                                var cellValue = row.ItemArray[col]?.ToString();
                                worksheet.Cells[lastRow, col + 1].Value = cellValue;
                            }

                            lastRow++;
                        }
                    }
                }

                lastRow = 8;

                foreach (DictionaryEntry entry in productFromOrder)
                {
                    long orderId = (long)entry.Key;
                    List<Product> products = (List<Product>)entry.Value;

                    var productsString = string.Join(", ", products.Select(p => p.title));

                    worksheet.Cells[lastRow, 7].Value = productsString;

                    lastRow++;
                }

                worksheet.Cells.AutoFitColumns();

                var tableRange = worksheet.Cells[7, 1, lastRow - 1, dataGrid.Columns.Count];
                var border = tableRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                lastRow++;
                worksheet.Cells[lastRow, 1].Value = "Составил: _______________";

                try
                {
                    excelPackage.Save();
                }
                catch (Exception)
                {
                    MessageBox.Show("Для открытия отчёта закройте Excel!");
                    return;
                }

            }

            Process.Start(filePath);
        }

        public void ExportDataTableToExcel(DataTable dataTable, string filePath, string title)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                if (excelPackage.Workbook.Worksheets.Any(x => x.Name.Equals("Карточка")))
                {
                    excelPackage.Workbook.Worksheets.Delete("Карточка");
                }

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Карточка");
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1:D1"].Value = title;
                worksheet.Cells["A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:D2"].Merge = true;
                worksheet.Cells["A2:D2"].Value = "ОАО Гомельский Мясокомбинат";
                worksheet.Cells["A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A3:D3"].Merge = true;
                worksheet.Cells["A3:D3"].Value = $"Продукт: {dataTable.Rows[0]["Название"].ToString()}";
                worksheet.Cells["A3:D3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A4:D4"].Merge = true;
                worksheet.Cells["A4:D4"].Value = "Главный склад";
                worksheet.Cells["A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A5:D5"].Merge = true;
                worksheet.Cells["A5:D5"].Value = "Адрес: Гомель, ул. Ильича, 2";
                worksheet.Cells["A5:D5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A6:D6"].Merge = true;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[7, i + 1].Value = dataTable.Columns[i].ColumnName;
                    worksheet.Column(i + 1).Width = 20;
                }

                int lastRow = 8;

                foreach (DataRow row in dataTable.Rows)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        var cellValue = row.ItemArray[col]?.ToString();
                        worksheet.Cells[lastRow, col + 1].Value = cellValue;
                    }

                    lastRow++;
                }

                var tableRange = worksheet.Cells[7, 1, lastRow - 1, dataTable.Columns.Count];
                var border = tableRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                lastRow++;
                worksheet.Cells[lastRow, 1].Value = "Составил: _______________";

                try
                {
                    excelPackage.Save();
                }
                catch (Exception)
                {
                    MessageBox.Show("Для открытия отчёта закройте Excel!");
                    return;
                }
            }

            Process.Start(filePath);
        }

        public void ExportDataTableToExcelOrder(DataTable supplierTable, DataTable orderTable, string filePath, string title, OrderedDictionary productFromOrder)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                if (excelPackage.Workbook.Worksheets.Any(x => x.Name.Equals(title)))
                {
                    excelPackage.Workbook.Worksheets.Delete(title);
                }

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(title);
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                worksheet.Cells["A1"].Value = title;
                worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2"].Value = "ОАО Гомельский Мясокомбинат";
                worksheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A3"].Value = "Главный склад";
                worksheet.Cells["A3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A4"].Value = "Адрес: Гомель, ул. Ильича, 2";
                worksheet.Cells["A4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                worksheet.Cells["A6"].Value = "Заказ:";
                worksheet.Cells["A6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                int lastRow = 7;

                for (int i = 0; i < orderTable.Columns.Count; i++)
                {
                    if (orderTable.Columns[i].ColumnName.Equals("Поставщик"))
                        continue;
                    var cellValue = orderTable.Columns[i].ColumnName + ": " + orderTable.Rows[0][i];
                    worksheet.Cells[lastRow, 1].Value = cellValue;
                    lastRow++;
                }

                foreach (DictionaryEntry entry in productFromOrder)
                {
                    long orderId = (long)entry.Key;
                    List<Product> products = (List<Product>)entry.Value;

                    var productsString = string.Join(", ", products.Select(p => p.title));

                    worksheet.Cells[lastRow, 1].Value = "Продукты: " + productsString;

                    lastRow++;
                }

                lastRow++;

                worksheet.Cells[$"A{lastRow}"].Value = "Поставщик:";
                worksheet.Cells[$"A{lastRow}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                lastRow++;
                int index = lastRow;

                for (int i = 0; i < supplierTable.Columns.Count; i++)
                {
                    var cellValue = supplierTable.Columns[i].ColumnName + ": " + supplierTable.Rows[0][i];
                    worksheet.Cells[lastRow, 1].Value = cellValue;
                    lastRow++;
                }

                worksheet.Cells[lastRow + 1, 1].Value = "Составил: _______________";

                worksheet.Cells.AutoFitColumns();

                try
                {
                    excelPackage.Save();
                }
                catch (Exception)
                {
                    MessageBox.Show("Для открытия отчёта закройте Excel!");
                    return;
                }

            }

            Process.Start(filePath);
        }

        public void Receipt(DataGrid dataGrid, string filePath, string title, OrderedDictionary productFromOrder, DateTime firstDate, DateTime secondDate)
        {
            ExcelFilter(dataGrid, filePath, title, productFromOrder, firstDate, secondDate, "Поступление");
        }

        public void Disposal(DataGrid dataGrid, string filePath, string title, OrderedDictionary productFromOrder, DateTime firstDate, DateTime secondDate)
        {
            ExcelFilter(dataGrid, filePath, title, productFromOrder, firstDate, secondDate, "Выбытие");
        }
    }
}