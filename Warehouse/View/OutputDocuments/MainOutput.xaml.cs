using MaterialDesignThemes.Wpf;
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
        private string ReceiptfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Receipt.xlsx";
        private string CardfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Card.xlsx";

        public MainOutput(DataGrid dataGrid)
        {
            InitializeComponent();

            outputService = new OutputService();

            database = new Database();

            this.dataGrid = dataGrid;
        }

        private void Disposal_Click(object sender, RoutedEventArgs e)
        {
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;
            OutputDoc.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Collapsed;

            FirstDate.Visibility = Visibility.Visible;
            SecondDate.Visibility = Visibility.Visible;
            ConfirmDisposal.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            PeriodLAbel.Visibility = Visibility.Visible;
        }

        private void Receipt_Click(object sender, RoutedEventArgs e)
        {
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;
            OutputDoc.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Collapsed;

            FirstDate.Visibility = Visibility.Visible;
            SecondDate.Visibility = Visibility.Visible;
            ConfirmReceipt.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            PeriodLAbel.Visibility = Visibility.Visible;
        }

        private void ProductFlowReport_Click(object sender, RoutedEventArgs e)
        {
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;
            OutputDoc.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Collapsed;

            FirstDate.Visibility = Visibility.Visible;
            SecondDate.Visibility = Visibility.Visible;
            ConfirmProduct.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            PeriodLAbel.Visibility = Visibility.Visible;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            outputService.ExportDataTableToExcel(database.GetOrderComposition(), CardfilePath, "Карточка складского учёта");
        }

        private void ReturnProduct_Click(object sender, RoutedEventArgs e)
        {
            ConfirmProduct.Visibility = Visibility.Collapsed;
            ReturnProduct.Visibility = Visibility.Collapsed;
            FirstDate.Visibility = Visibility.Collapsed;
            SecondDate.Visibility = Visibility.Collapsed;
            PeriodLAbel.Visibility = Visibility.Collapsed;
            ConfirmReceipt.Visibility = Visibility.Collapsed;
            ConfirmDisposal.Visibility = Visibility.Collapsed;

            ProductFlowReport.Visibility = Visibility.Visible;
            Receipt.Visibility = Visibility.Visible;
            Disposal.Visibility = Visibility.Visible;
            OutputDoc.Visibility = Visibility.Visible;
            Card.Visibility = Visibility.Visible;
        }

        private void ConfirmProduct_Click(object sender, RoutedEventArgs e)
        {
            DateTime firstDate = FirstDate.SelectedDate.Value;
            DateTime secondDate = SecondDate.SelectedDate.Value;

            outputService.ExportToExcel(dataGrid, filePath, "Отчёт о движении продуктов", database.ProductIdDate(firstDate, secondDate), firstDate, secondDate);
        }

        private void ConfirmReceipt_Click(object sender, RoutedEventArgs e)
        {
            DateTime firstDate = FirstDate.SelectedDate.Value;
            DateTime secondDate = SecondDate.SelectedDate.Value;

            outputService.Receipt(dataGrid, ReceiptfilePath, "Реестр документов по поступлению", database.ProductIdDateWithType(firstDate, secondDate, "Поступление"), firstDate, secondDate);
        }

        private void ConfirmDisposal_Click(object sender, RoutedEventArgs e)
        {
            DateTime firstDate = FirstDate.SelectedDate.Value;
            DateTime secondDate = SecondDate.SelectedDate.Value;

            outputService.Disposal(dataGrid, DisposalfilePath, "Реестр документов по выбытию", database.ProductIdDateWithType(firstDate, secondDate, "Выбытие"), firstDate, secondDate);
        }
    }
}