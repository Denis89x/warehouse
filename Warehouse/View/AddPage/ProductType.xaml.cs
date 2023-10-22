using System.Windows;
using System.Windows.Controls;
using Warehouse.Service;

namespace Warehouse.View.AddPage
{

    public partial class ProductType : Window
    {
        DataGrid data;

        public ProductType(DataGrid data)
        {
            this.data = data;
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = ProductTypeBox.Text;

            ValidationFileds validation = new ValidationFileds();
            if (validation.ValidationProductTypeTitle(title))
            {
                Database database = new Database();

                database.CreateProductType(title);
                database.ReadProductType(data);
            }
        }
    }
}
