using System.Windows;

namespace Warehouse.View.Add
{
    public partial class ProductTypeAdd : Window
    {
        public ProductTypeAdd()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
