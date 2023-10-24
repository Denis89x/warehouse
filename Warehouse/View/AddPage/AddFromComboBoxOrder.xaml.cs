using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class AddFromComboBoxOrder : Window
    {
        double amount = 0;

        DataGrid grid;
        TextBox TextBox;
        Database database = new Database();

        public AddFromComboBoxOrder(DataGrid grid, TextBox textBox)
        {
            InitializeComponent();
            this.grid = grid;
            this.TextBox = textBox;
            database.ReadProductToComboBox(ProductComboBox);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO) ProductComboBox.SelectedItem;
            string quantity = ProductQuantity.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationAddFromComboBoxOrder(quantity, dto))
            {
                if (!ComboBoxOrder.dicrtionaryWithName.ContainsKey(dto.name))
                { 
                    ComboBoxOrder.dicrtionaryWithId1.Add(dto.id, validation.CastQuantityToInt(quantity));
                    ComboBoxOrder.dicrtionaryWithName.Add(dto.name, validation.CastQuantityToInt(quantity));

                    addToComboBoxOrder(ComboBoxOrder.dicrtionaryWithName);

                    AddAmount();

                    this.Close();
                } else
                {
                    MessageBox.Show("Данный товар уже выбран!");
                }
            }
        }

        private void addToComboBoxOrder(Dictionary<string, int> dictionary)
        {

            foreach (var item in dictionary)
            {
                Database.combo.Add(new ComboBoxOrder { Title = item.Key, Quantity = item.Value });
            }

            grid.ItemsSource = Database.combo;
        }

        private void AddAmount()
        {
            List<long> ids = new List<long>();

            foreach (DictionaryEntry item in ComboBoxOrder.dicrtionaryWithId1)
            {
                ids.Add((long)item.Key);
            }

            int i = 0;

            foreach (var item in grid.Items)
            {
                int quantity = ((ComboBoxOrder)item).Quantity;

                int cost = database.ReadProductCostById(ids[i]);
                double subtotal = quantity * cost;
                amount += subtotal;

                i++;
            }

            TextBox.Text = amount.ToString();
        }
    }
}
