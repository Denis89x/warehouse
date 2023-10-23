using System.Windows;
using System.Windows.Controls;
using Warehouse.Service;

namespace Warehouse.View.AddPage
{
    public partial class SupplierAdd : Window
    {
        DataGrid grid;

        public SupplierAdd(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = SupplierTitleBox.Text;
            string address = SupplierAdressBox.Text;
            string phone = SupplierPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationSupplierAdd(title, address, phone, surname, firstName, middleName))
            {
                Database database = new Database();
                database.CreateSupplier(title, address, phone, surname, firstName, middleName);
                database.ReadSupplier(grid);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            SupplierTitleBox.Visibility = Visibility.Collapsed;
            SupplierAdressBox.Visibility = Visibility.Collapsed;
            SupplierPhoneBox.Visibility = Visibility.Collapsed;
            Next.Visibility = Visibility.Collapsed;
            Return.Visibility = Visibility.Collapsed;

            SurnameBox.Visibility = Visibility.Visible;
            FirstNameBox.Visibility = Visibility.Visible;
            MiddleNameBox.Visibility = Visibility.Visible;
            Preview.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            SurnameBox.Visibility = Visibility.Collapsed;
            FirstNameBox.Visibility = Visibility.Collapsed;
            MiddleNameBox.Visibility = Visibility.Collapsed;
            Preview.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;

            SupplierTitleBox.Visibility = Visibility.Visible;
            SupplierAdressBox.Visibility = Visibility.Visible;
            SupplierPhoneBox.Visibility = Visibility.Visible;
            Next.Visibility = Visibility.Visible;
            Return.Visibility = Visibility.Visible;
        }
    }
}
