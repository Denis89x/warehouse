using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Service;

namespace Warehouse.View.AddPage
{
    public partial class OrderAdd : Window
    {
        Database database = new Database();
        ValidationFileds validation = new ValidationFileds();
        DataGrid grid = new DataGrid();

        public OrderAdd(DataGrid grid)
        {
            InitializeComponent();
            database.ReadSupplierToComboBox(SupplierComboBox);
            this.grid = grid;
            Date.SelectedDate = DateTime.Today;
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddFromComboBoxOrder add = new AddFromComboBoxOrder(SupplierGrid, ProductCost);
            add.ShowDialog();
        }

        private void NextToAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReturnToFirstPage_Click(object sender, RoutedEventArgs e)
        {
            CompleteButton.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;
            ReturnToFirstPage.Visibility = Visibility.Collapsed;

            SupplierComboBox.Visibility = Visibility.Visible;
            OrderTypeComboBox.Visibility = Visibility.Visible;
            ProductCost.Visibility = Visibility.Visible;
            Date.Visibility = Visibility.Visible;
            NextToSecondPage.Visibility = Visibility.Visible;
            ReturnToMain.Visibility = Visibility.Visible;
        }

        private void NextToSecondPage_Click(object sender, RoutedEventArgs e)
        {
            SupplierComboBox.Visibility = Visibility.Collapsed;
            OrderTypeComboBox.Visibility = Visibility.Collapsed;
            ProductCost.Visibility = Visibility.Collapsed;
            Date.Visibility = Visibility.Collapsed;
            NextToSecondPage.Visibility = Visibility.Collapsed;
            ReturnToMain.Visibility = Visibility.Collapsed;

            CompleteButton.Visibility = Visibility.Visible;
            SupplierGrid.Visibility = Visibility.Visible;
            ReturnToFirstPage.Visibility = Visibility.Visible;
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO supplier = (ComboBoxDTO)SupplierComboBox.SelectedItem;
            string orderDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationComboBoxProduct(supplier, "поставщика") && validation.ValidationComboBox(((ComboBoxItem)OrderTypeComboBox.SelectedItem).Content.ToString(), "Тип заказа") && validation.ValidateAmount(ProductCost.Text))
            {
                database.CreateOrder(supplier, validation.CastCostToDouble(ProductCost.Text), ((ComboBoxItem)OrderTypeComboBox.SelectedItem).Content.ToString(), orderDate);
                DataTable orderTable = database.GetOrdersWithProducts();
                grid.ItemsSource = orderTable.DefaultView;
                this.Close();
            }
        }
    }
}
