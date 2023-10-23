using System.Windows;

namespace Warehouse.View.AddPage
{
    public partial class OrderAdd : Window
    {
        public OrderAdd()
        {
            InitializeComponent();
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {

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

        }
    }
}
