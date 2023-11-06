using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.Profile;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.AddPage;
using Warehouse.View.EditPage;
using Warehouse.View.FIltrationAndSearch;
using Warehouse.View.OutputDocuments;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        private Database database = new Database();
        private MainLogic mainLogic;

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

            mainLogic = new MainLogic();

            DataTable orderTable = database.GetOrdersWithProducts();
            OrderGrid.ItemsSource = orderTable.DefaultView;
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
            SupplierGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;

            ProductTypeGrid.Visibility = Visibility.Visible;

            database.ReadProductType(ProductTypeGrid);
        }

        private void Product_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            ProductTypeGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;

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
            OrderGrid.Visibility = Visibility.Collapsed;

            SupplierGrid.Visibility = Visibility.Visible;

            database.ReadSupplier(SupplierGrid);
        }

        private void Order_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;

            OrderGrid.Visibility = Visibility.Visible;

            DataTable orderTable = database.GetOrdersWithProducts();
            OrderGrid.ItemsSource = orderTable.DefaultView;
        }

        private void OrderButtontetet_Click(object sender, RoutedEventArgs e)
        {
            OrderAdd order = new OrderAdd(OrderGrid);
            order.Show();
            ComboBoxOrder.dicrtionaryWithId1.Clear();
            ComboBoxOrder.dicrtionaryWithName.Clear();
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = SupplierGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    database.DeleteSupplier(selectedRow);
                } catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим поставщиком!");
                    return;
                }

                database.ReadSupplier(SupplierGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = ProductGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    database.DeleteProduct(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим продуктом!");
                    return;
                }

                database.ReadProduct(ProductGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void DeleteProductType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = ProductTypeGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    database.DeleteProductType(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим типом продукта!");
                    return;
                }

                database.ReadProductType(ProductTypeGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = OrderGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    database.DeleteOrder(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные!");
                    return;
                }

                DataTable orderTable = database.GetOrdersWithProducts();
                OrderGrid.ItemsSource = orderTable.DefaultView;
            } 
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = SupplierGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                SupplierEdit supplierEdit = new SupplierEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]), Convert.ToString(selectedRow.Row.ItemArray[6]), SupplierGrid);
                supplierEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ProductGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                ProductEdit productEdit = new ProductEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[5]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), ProductGrid);
                productEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void EditProductType_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ProductTypeGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                ProductTypeEdit productTypeEdit = new ProductTypeEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), ProductTypeGrid);
                productTypeEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void FiltrationOrder_Click(object sender, RoutedEventArgs e)
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, OrderGrid);
        }

        private void FiltrationProductType_Click(object sender, RoutedEventArgs e)
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, ProductTypeGrid);
        }

        private void FiltrationProduct_Click(object sender, RoutedEventArgs e)
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, ProductGrid);
        }

        private void FiltrationSupplier_Click(object sender, RoutedEventArgs e)
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, SupplierGrid);
        }

        private void SearchOrder_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, OrderGrid);
        }

        private void SearchProductType_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, ProductTypeGrid);
        }

        private void SearchSupplier_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, SupplierGrid);
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, ProductGrid);
        }

        private void OrderCancel_Click(object sender, RoutedEventArgs e)
        {
            DataTable orderTable = database.GetOrdersWithProducts();
            OrderGrid.ItemsSource = orderTable.DefaultView;
        }

        private void ProductTypeCancel_Click(object sender, RoutedEventArgs e)
        {
            database.ReadProductType(ProductTypeGrid);
        }

        private void ProductCancel_Click(object sender, RoutedEventArgs e)
        {
            database.ReadProduct(ProductGrid);
        }

        private void SupplierCancel_Click(object sender, RoutedEventArgs e)
        {
            database.ReadSupplier(SupplierGrid);
        }

        private void Output_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainOutput main = new MainOutput(OrderGrid);
            main.ShowDialog();
        }
    }
}