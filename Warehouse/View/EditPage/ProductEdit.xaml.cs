using System.Windows;
using System.Windows.Controls;

namespace Warehouse.View.EditPage
{
    public partial class ProductEdit : Window
    {
        DataGrid grid;
        Database database = new Database();

        public ProductEdit(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
        }
    }
}
