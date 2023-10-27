using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Warehouse.Service
{
    internal class MainLogic
    {
        public MainLogic() { }

        public void ApplyFilter(string field, DataGrid dataGrid)
        {
            foreach (var item in dataGrid.Items)
            {
                var row = dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                bool isVisible = false;

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    var cellContent = dataGrid.Columns[i].GetCellContent(item);
                    if (cellContent is TextBlock textBlock && textBlock.Text.ToLower().Contains(field.ToLower()))
                    {
                        isVisible = true;
                        break;
                    }
                }

                if (row != null)
                {
                    row.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public void SearchAndSort(string searchValue, DataGrid dataGrid)
        {
            var items = dataGrid.Items.Cast<object>().ToList();

            items.Sort((item1, item2) =>
            {
                double similarity1 = CalculateSimilarity(item1, searchValue, dataGrid);
                double similarity2 = CalculateSimilarity(item2, searchValue, dataGrid);

                return similarity2.CompareTo(similarity1);
            });

            dataGrid.ItemsSource = items;
        }

        private double CalculateSimilarity(object item, string searchValue, DataGrid dataGrid)
        {
            double similarity = 0;

            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                var cellContent = dataGrid.Columns[i].GetCellContent(item);
                if (cellContent is TextBlock textBlock)
                {
                    string cellValue = textBlock.Text;
                    double cellSimilarity = CalculateSimilarityValue(cellValue, searchValue);
                    similarity += cellSimilarity;
                }
            }

            return similarity;
        }

        private double CalculateSimilarityValue(string value1, string value2)
        {
            int maxLength = Math.Max(value1.Length, value2.Length);
            int[,] matrix = new int[value1.Length + 1, value2.Length + 1];

            for (int i = 0; i <= value1.Length; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j <= value2.Length; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= value1.Length; i++)
            {
                for (int j = 1; j <= value2.Length; j++)
                {
                    int cost = (value1[i - 1] == value2[j - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }

            int distance = matrix[value1.Length, value2.Length];
            double similarity = 1 - (double)distance / maxLength;

            return similarity;
        }


    }
}
