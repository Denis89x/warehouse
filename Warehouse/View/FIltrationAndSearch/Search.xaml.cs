using System.Windows;

namespace Warehouse.View.FIltrationAndSearch
{
    public partial class Search : Window
    {
        public string Field { get; set; }

        public Search()
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
