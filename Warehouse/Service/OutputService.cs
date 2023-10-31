﻿using OfficeOpenXml;
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

                var usedRange = worksheet.Cells[worksheet.Dimension.Address];
                var border = usedRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                try
                {
                    excelPackage.Save();
                } catch (Exception)
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

                var usedRange = worksheet.Cells[worksheet.Dimension.Address];
                var border = usedRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

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

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A1:C1"].Value = title;
                worksheet.Cells["A1:C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A2:C2"].Value = "ОАО Гомельский Мясокомбинат";
                worksheet.Cells["A2:C2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A3:C3"].Value = "Главный склад";
                worksheet.Cells["A3:C3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A4:C4"].Value = "Адрес: Гомель, ул. Ильича, 2";
                worksheet.Cells["A4:C4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A5:C5"].Merge = true;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[6, i + 1].Value = dataTable.Columns[i].ColumnName;
                    worksheet.Column(i + 1).Width = 20; // Установка ширины ячейки
                }

                int lastRow = 7;

                foreach (DataRow row in dataTable.Rows)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        var cellValue = row.ItemArray[col]?.ToString();
                        worksheet.Cells[lastRow, col + 1].Value = cellValue;
                    }

                    lastRow++;
                }

                var usedRange = worksheet.Cells[worksheet.Dimension.Address];
                var border = usedRange.Style.Border;
                border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

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