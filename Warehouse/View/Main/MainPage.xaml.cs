﻿using System.Windows;
using Warehouse.Profile;
using Warehouse.Storage;
using Warehouse.View.AddPage;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        Database database = new Database();

        public MainPage()
        {
            InitializeComponent();
            if (AuthManager.CurrentUsername != null)
            {
                bool isAdmin = database.CheckAdmin(AuthManager.CurrentUsername);
                if (!isAdmin)
                {
                    AdminRegistration.Visibility = Visibility.Collapsed;
                } else
                {
                    AdminRegistration.Visibility = Visibility.Visible;
                }
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void AdminRegistration_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddProductType_Click(object sender, RoutedEventArgs e)
        {
            ProductType product = new ProductType(ProductTypeGrid);
            product.ShowDialog();
        }

        private void Settings_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PrfSettings settings = new PrfSettings();
            settings.ShowDialog();
        }

        private void ProductType_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProductGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;

            ProductTypeGrid.Visibility = Visibility.Visible;

            database.ReadProductType(ProductTypeGrid);
        }

        private void OrderStructure_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Product_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;

            ProductGrid.Visibility = Visibility.Visible;

            database.ReadProduct(ProductGrid);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductAdd product = new ProductAdd(ProductGrid);
            product.ShowDialog();
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            SupplierAdd supplierAdd = new SupplierAdd(SupplierGrid);
            supplierAdd.ShowDialog();
        }

        private void Supplier_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProductGrid.Visibility = Visibility.Collapsed;
            ProductTypeGrid.Visibility = Visibility.Collapsed;

            SupplierGrid.Visibility = Visibility.Visible;

            database.ReadSupplier(SupplierGrid);
        }

        private void Order_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void OrderButtontetet_Click(object sender, RoutedEventArgs e)
        {
            OrderAdd order = new OrderAdd();
            order.Show();
            this.Hide();
        }
    }
}
