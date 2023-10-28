using System.Windows;
using System.Windows.Controls;
using Warehouse.Service;

namespace Warehouse.View.OutputDocuments
{

    public partial class MainOutput : Window
    {
        private OutputService outputService;

        DataGrid dataGrid;
        Database database;

        private string filePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Template.xlsx";

        public MainOutput(DataGrid dataGrid)
        {
            InitializeComponent();

            outputService = new OutputService();

            database = new Database();

            this.dataGrid = dataGrid;
        }

        private void Disposal_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Receipt_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProductFlowReport_Click(object sender, RoutedEventArgs e)
        {
            outputService.ExportToExcel(dataGrid, filePath, "Отчёт о движении продуктов", database.GetProductWithOrderId());
        }
    }
}
