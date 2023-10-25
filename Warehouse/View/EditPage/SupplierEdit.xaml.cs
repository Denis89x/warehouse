using System.Windows;
using System.Windows.Controls;
using Warehouse.Service;

namespace Warehouse.View.EditPage
{
    public partial class SupplierEdit : Window
    {
        long id;
        DataGrid grid;

        public SupplierEdit(long id, string title, string address, string phoneNumber, string surname, string firstName, string middleName, DataGrid grid)
        {
            InitializeComponent();
            this.id = id;
            this.grid = grid;
            SupplierTitleBox.Text = title;
            SupplierAdressBox.Text = address;
            SupplierPhoneBox.Text = phoneNumber;
            SurnameBox.Text = surname;
            FirstNameBox.Text = firstName;
            MiddleNameBox.Text = middleName;
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
                database.UpdateSupplier(id, title, address, phone, surname, firstName, middleName);
                database.ReadSupplier(grid);

                this.Close();
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
