using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Warehouse.Service;

namespace Warehouse.View.EditPage
{
    public partial class ProductTypeEdit : Window
    {
        long id;
        DataGrid grid;

        public ProductTypeEdit(long id, string title, DataGrid grid)
        {
            InitializeComponent();
            this.id = id;
            this.grid = grid;
            ProductTypeBox.Text = title;
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

                database.UpdateProductType(id, title);
                database.ReadProductType(grid);

                this.Close();
            }
        }
    }
}
