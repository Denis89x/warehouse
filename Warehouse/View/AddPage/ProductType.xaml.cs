using System.Windows;

namespace Warehouse.View.AddPage
{

    public partial class ProductType : Window
    {
        public ProductType()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
