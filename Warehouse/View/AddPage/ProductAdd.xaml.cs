using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Service;

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
            string title = ProductTitleBox.Text;
            ComboBoxDTO dto = (ComboBoxDTO) ProductTypeComboBox.SelectedItem;
            string cost = ProductCost.Text;
            string description = ProductDescription.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationProductAdd(title, cost, description, dto))
            {
                database.CreateProduct(title, validation.CastCostToDouble(cost), description, dto);
                database.ReadProduct(grid);
            }
        }
    }
}
