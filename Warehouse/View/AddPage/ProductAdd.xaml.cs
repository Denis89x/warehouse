using System.Windows;
using System.Windows.Controls;

namespace Warehouse.View.AddPage
{
    public partial class ProductAdd : Window
    {
        DataGrid grid;
        Database database = new Database();

        public ProductAdd(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            database.ReadProductTypeToComboBox(ProductTypeComboBox);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
