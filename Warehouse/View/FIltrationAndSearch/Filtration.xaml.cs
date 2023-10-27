using System.Windows;

namespace Warehouse.View.FIltrationAndSearch
{
    public partial class Filtration : Window
    {
        public string Field { get; set; }

        public Filtration()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Field = fieldBox.Text;
            this.Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
