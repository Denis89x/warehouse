using System.Windows;
using System.Windows.Controls;
using Warehouse.Service;

namespace Warehouse.View.EditPage
{
    public partial class ProductEdit : Window
    {
        DataGrid grid;
        Database database = new Database();
        int id;

        public ProductEdit(int id, string title, string productType, string cost, string description, DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            this.id = id;
            ProductTitleBox.Text = title;
            ProductTypeComboBox.Items.Add(productType);
            ProductTypeComboBox.SelectedIndex = 0;
            ProductCost.Text = cost;
            ProductDescription.Text = description;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = ProductTitleBox.Text;
            string cost = ProductCost.Text;
            string description = ProductDescription.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationProductEdit(title, cost, description))
            {
                database.UpdateProduct(id, title, validation.CastCostToDouble(cost), description);
                database.ReadProduct(grid);

                this.Close();
            }
        }
    }
}
