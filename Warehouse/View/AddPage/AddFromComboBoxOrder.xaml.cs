using System.Windows;
using System.Windows.Controls;

namespace Warehouse.View.AddPage
{
    public partial class AddFromComboBoxOrder : Window
    {
        DataGrid grid;
        Database database = new Database();

        public AddFromComboBoxOrder(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            database.ReadProductToComboBox(ProductComboBox);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
