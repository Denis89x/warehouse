using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Service;

namespace Warehouse.View.OutputDocuments
{
    public partial class MainOutput : Window
    {
        private OutputService outputService;
        private ValidationFileds validationFileds;

        DataGrid dataGrid;
        Database database;

        private string filePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Template.xlsx";
        private string DisposalfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Disposal.xlsx";
        private string ReceiptfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Receipt.xlsx";
        private string CardfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Card.xlsx";
        private string OrderfilePath = "C:\\Производственная практика\\Warehouse\\Warehouse\\Resources\\Order.xlsx";

        public MainOutput(DataGrid dataGrid)
        {
            InitializeComponent();

            outputService = new OutputService();

            database = new Database();

            database.ReadProductToComboBox(ProductComboBox);
            database.ReadOrderToComboBox(OrderComboBox);

            this.dataGrid = dataGrid;
            validationFileds = new ValidationFileds();
            SecondDate.SelectedDate = DateTime.Today;
            FirstDate.SelectedDate = DateTime.Today;
        }

        private void Disposal_Click(object sender, RoutedEventArgs e)
        {
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;
            OutputDoc.Visibility = Visibility.Collapsed;
            Order.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;

            SecondBorder.Visibility = Visibility.Visible;
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
            Order.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;

            SecondBorder.Visibility = Visibility.Visible;

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
            Order.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;

            SecondBorder.Visibility = Visibility.Visible;

            FirstDate.Visibility = Visibility.Visible;
            SecondDate.Visibility = Visibility.Visible;
            ConfirmProduct.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            PeriodLAbel.Visibility = Visibility.Visible;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            Order.Visibility = Visibility.Collapsed;
            OutputDoc.Visibility = Visibility.Collapsed;

            SecondBorder.Visibility = Visibility.Visible;
            ConfirmCard.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            ProductComboBox.Visibility = Visibility.Visible;
            OutCardAndOrder.Visibility = Visibility.Visible;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            ProductFlowReport.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            Disposal.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            Order.Visibility = Visibility.Collapsed;
            OutputDoc.Visibility = Visibility.Collapsed;

            SecondBorder.Visibility = Visibility.Visible;
            OrderComboBox.Visibility = Visibility.Visible;
            ConfirmOrder.Visibility = Visibility.Visible;
            ReturnProduct.Visibility = Visibility.Visible;
            OutCardAndOrder.Visibility = Visibility.Visible;
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
            ConfirmCard.Visibility = Visibility.Collapsed;
            ProductComboBox.Visibility = Visibility.Collapsed;
            SecondBorder.Visibility = Visibility.Collapsed;
            OrderComboBox.Visibility = Visibility.Collapsed;
            ConfirmOrder.Visibility = Visibility.Collapsed;
            OutCardAndOrder.Visibility = Visibility.Collapsed;

            Order.Visibility = Visibility.Visible;
            MainBorder.Visibility = Visibility.Visible;
            ProductFlowReport.Visibility = Visibility.Visible;
            Receipt.Visibility = Visibility.Visible;
            Disposal.Visibility = Visibility.Visible;
            OutputDoc.Visibility = Visibility.Visible;
            Card.Visibility = Visibility.Visible;
        }

        private void ConfirmProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validationFileds.ValidationDatePeriod(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value))
                {
                    outputService.ExportToExcel(dataGrid, filePath, "Отчёт о движении продуктов", database.ProductIdDate(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value), FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }


        private void ConfirmReceipt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validationFileds.ValidationDatePeriod(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value))
                {
                    outputService.Receipt(dataGrid, ReceiptfilePath, "Реестр документов по поступлению", database.ProductIdDateWithType(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value, "Поступление"), FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }

        private void ConfirmDisposal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validationFileds.ValidationDatePeriod(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value))
                {
                    outputService.Disposal(dataGrid, DisposalfilePath, "Реестр документов по выбытию", database.ProductIdDateWithType(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value, "Выбытие"), FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }

        private void ConfirmCard_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO product = (ComboBoxDTO)ProductComboBox.SelectedItem;

            if (validationFileds.ValidationComboBoxProduct(product, "продукт"))
            {
                outputService.ExportDataTableToExcel(database.GetOrderComposition(product.name), CardfilePath, "Карточка складского учёта");

            }
        }

        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO order = (ComboBoxDTO)OrderComboBox.SelectedItem;

            if (validationFileds.ValidationComboBoxProduct(order, "заказ"))
            {
                outputService.ExportDataTableToExcelOrder(database.GetSupplier(database.GetSupplierId(order.id)), database.GetOrderWithOrderId(order.id), OrderfilePath, $"Информация о заказе: {order.id}", database.ProductsForOrder(order.id));
            }
        }
    }
}