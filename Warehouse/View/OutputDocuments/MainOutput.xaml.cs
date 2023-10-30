using System;
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
        private string DisposalfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Disposal.xlsx";

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
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;

            FirstDate.Visibility = Visibility.Visible;
            SecondDate.Visibility = Visibility.Visible;
            ConfirmProduct.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            PeriodLAbel.Visibility = Visibility.Visible;
        }

        private void ReturnProduct_Click(object sender, RoutedEventArgs e)
        {
            ConfirmProduct.Visibility = Visibility.Collapsed;
            ReturnProduct.Visibility = Visibility.Collapsed;
            FirstDate.Visibility = Visibility.Collapsed;
            SecondDate.Visibility = Visibility.Collapsed;
            PeriodLAbel.Visibility = Visibility.Collapsed;

            ProductFlowReport.Visibility = Visibility.Visible;
            Receipt.Visibility = Visibility.Visible;
            Disposal.Visibility = Visibility.Visible;
        }

        private void ConfirmProduct_Click(object sender, RoutedEventArgs e)
        {
            DateTime firstDate = FirstDate.SelectedDate.Value;
            DateTime secondDate = SecondDate.SelectedDate.Value;

            outputService.ExportToExcel(dataGrid, filePath, "Отчёт о движении продуктов", database.GetProductWithOrderId(firstDate, secondDate), firstDate, secondDate);
        }
    }
}
