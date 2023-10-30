using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Model;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse
{
    internal class Database
    {
        static string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=warehouse;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        SqlConnection sqlConnection = new SqlConnection(connection);
        private string selectProductType = $"select product_type_id, type_name from product_type";
        private string selectProductAll = $"select product_id, title from product";
        private string selectProduct = $"select product.product_id, product_type.type_name, product.presence, product.cost, product.description, product.title  from product, product_type WHERE product.product_type_id = product_type.product_type_id";
        private string selectSupplier = $"select * from supplier";
        private string selectSupplierAll = $"select supplier_id, title from supplier";

        public void Connection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public DataTable Select(string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public void Select(string query, DataGrid dataGrid)
        {
            Connection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            Connection();
        }

        public int ReadProductCostById(long id)
        {
            Connection();

            SqlCommand command = new SqlCommand($"Select cost from product where product_id = {id}", sqlConnection);

            int cost = Convert.ToInt32(command.ExecuteScalar());

            Connection();

            return cost;
        }

        public int CountUsersWithLogin(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}'", sqlConnection);
            int count = (int)command.ExecuteScalar();
            Connection();
            return count;
        }

        public bool Check(string username, string password)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}' AND Password='{PasswordEncoder.GetSHA256Hash(password)}'", sqlConnection);
            int result = (int)command.ExecuteScalar();
            Connection();

            return result > 0;
        }

        public bool CheckAdmin(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}' AND role='ROLE_ADMIN'", sqlConnection);
            int result = (int)command.ExecuteScalar();
            Connection();

            return result > 0;
        }

        public void RegisterAccount(string username, string password, string role, string surname, string firstName, string middleName)
        {
            string query = $"INSERT INTO Account (username, password, role, surname, first_name, middle_name) VALUES ('{username.ToLower()}', '{PasswordEncoder.GetSHA256Hash(password)}', '{role}', N'{surname.ToLower()}', N'{firstName.ToLower()}', N'{middleName.ToLower()}')";
            Update(query);
        }

        public void UpdateUsername(string username)
        {
            string query = $"UPDATE Account SET username = '{username.ToLower()}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}'";
            Update(query);
        }

        public void UpdatePassword(string password)
        {
            string query = $"UPDATE Account SET password = '{PasswordEncoder.GetSHA256Hash(password)}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}'";
            Update(query);
        }

        public void UpdateUsernameAndPassword(string username, string password)
        {
            string query = $"UPDATE Account SET username = '{username.ToLower()}', password = '{password}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}' AND password = '{PasswordEncoder.GetSHA256Hash(password)}'";
            Update(query);
        }

        public void Update(string query)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                Connection();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.ExecuteNonQuery();
            Connection();
        }

        public OrderedDictionary GetProductWithOrderId(DateTime firstDate, DateTime secondDate)
        {
            OrderedDictionary productFromOrder = new OrderedDictionary();

            MessageBox.Show($"firstDate : {firstDate}, secondDate: {secondDate}");
            MessageBox.Show($"firstDate : {firstDate.ToString("yyyy-MM-dd")}, secondDate: {secondDate.ToString("yyyy-MM-dd")}");

            string query = $@"
                            SELECT ord.order_id, product.product_id, product.title
                            FROM ord
                            JOIN order_composition ON ord.order_id = order_composition.order_id
                            JOIN product ON order_composition.product_id = product.product_id
                            WHERE ord.order_date >= '{firstDate.ToString("yyyy-MM-dd")}' AND ord.order_date <= '{secondDate.ToString("yyyy-MM-dd")}'";

            DataTable result = Select(query);

            foreach (DataRow row in result.Rows)
            {
                long orderId = (long)row["order_id"];
                long productId = (long)row["product_id"];
                string title = row["title"].ToString();

                // Создание объекта Product
                Product product = new Product
                {
                    id = productId,
                    title = title,
                    // Другие свойства продукта
                };

                // Добавление товара в список для данного заказа
                if (productFromOrder.Contains(orderId))
                {
                    List<Product> products = (List<Product>)productFromOrder[orderId];
                    products.Add(product);
                }
                else
                {
                    List<Product> products = new List<Product> { product };
                    productFromOrder.Add(orderId, products);
                }
            }

            return productFromOrder;
        }

        public void ShowProductFromOrder(OrderedDictionary productFromOrder)
        {
            StringBuilder message = new StringBuilder();

            foreach (DictionaryEntry entry in productFromOrder)
            {
                long orderId = (long)entry.Key;
                List<Product> products = (List<Product>)entry.Value;

                message.AppendLine($"Order ID: {orderId}");

                foreach (Product product in products)
                {
                    message.AppendLine($"Product ID: {product.id}, Title: {product.title}");
                    // Добавьте другие свойства продукта, если необходимо
                }

                message.AppendLine(); // Пустая строка между заказами
            }

            MessageBox.Show(message.ToString());
        }

        public void CreateProductType(string title)
        {
            Update($"insert into product_type (type_name) values (N'{title.ToLower()}')");
        }

        public void UpdateProductType(long id, string title)
        {
            Update($"update product_type set type_name = N'{title}' where product_type_id = '{id}'");
        }

        public void CreateProduct(string title, double cost, string description, ComboBoxDTO dto)
        {
            Update($"insert into product (product_type_id, presence, cost, description, title) values ('{dto.id}', '{0}', '{cost}', N'{description}', N'{title}')");
        }

        public void UpdateProduct(long id, string title, double cost, string description)
        {
            Update($"update product set title = N'{title}', cost = '{cost}', description = N'{description}' where product_id = '{id}'");
        }

        public void CreateSupplier(string title, string address, string phoneNumber, string surname, string firstName, string middleName)
        {
            Update($"insert into supplier (title, address, phone_number, surname, first_name, middle_name) values (N'{title}', N'{address}', '{phoneNumber}', N'{surname}', N'{firstName}', N'{middleName}')");
        }

        public void UpdateSupplier(long id, string title, string address, string phoneNumber, string surname, string firstName, string middleName)
        {
            Update($"update supplier set title = N'{title}', address = N'{address}', phone_number = '{phoneNumber}', surname = N'{surname}', first_name = N'{firstName}', middle_name = N'{middleName}' where supplier_id = '{id}'");
        }

        public long GetAccountId()
        {
            SqlCommand command = new SqlCommand($"Select account_id from account where username = '{AuthManager.CurrentUsername}'", sqlConnection);

            long id = Convert.ToInt64(command.ExecuteScalar());

            return id;
        }

        public long GetOrderId()
        {
            Connection();

            SqlCommand command = new SqlCommand($"Select order_id from ord where username = {AuthManager.CurrentUsername}", sqlConnection);

            long id = Convert.ToInt64(command.ExecuteScalar());

            Connection();

            return id;
        }

        public void CreateOrder(ComboBoxDTO supplierDTO, double amount, string orderType, string orderDate)
        {
            Connection();

            if (orderType.Equals("Выбытие"))
            {
                foreach (DictionaryEntry item in ComboBoxOrder.dicrtionaryWithId1)
                {
                    SqlCommand commandToQuantity = new SqlCommand($"SELECT presence FROM product WHERE product_id = '{(long)item.Key}'", sqlConnection);
                    decimal presence = (decimal)commandToQuantity.ExecuteScalar();

                    if (presence < (int)item.Value)
                    {
                        MessageBox.Show($"Количество на убытие указано не верно! Текущий остаток: {presence}");
                        return;
                    }
                }
            }

            SqlCommand command = new SqlCommand($"insert into ord (supplier_id, account_id, amount, order_date, order_type) output inserted.order_id values ('{supplierDTO.id}', '{GetAccountId()}', '{amount}', '{orderDate}', N'{orderType}')", sqlConnection);
            long orderId = (long)command.ExecuteScalar();

            foreach (DictionaryEntry item in ComboBoxOrder.dicrtionaryWithId1)
            {
                Update($"insert into order_composition (product_id, order_id, quantity) values ('{(long)item.Key}', '{orderId}', '{(int)item.Value}')");
                if (orderType.Equals("Поступление"))
                    Update($"UPDATE product set presence = presence + '{(int)item.Value}' where product_id = '{(long)item.Key}'");
                if (orderType.Equals("Выбытие"))
                    Update($"UPDATE product set presence = presence - '{(int)item.Value}' where product_id = '{(long)item.Key}'");
            }

            Connection();
        }

        public void DeleteOrder(DataRowView selectedRow)
        {
            Connection();

            long orderId = (long)selectedRow.Row.ItemArray[0];

            string orderType = GetOrderType(orderId);

            Dictionary<long, int> products = GetOrderCompositionProducts(orderId);

            foreach (var product in products)
            {
                if (orderType.Equals("Поступление"))
                    Update($"UPDATE product SET presence = presence - {product.Value} WHERE product_id = {product.Key}");
                if (orderType.Equals("Выбытие"))
                    Update($"UPDATE product SET presence = presence + {product.Value} WHERE product_id = {product.Key}");
            }

            Update($"DELETE FROM order_composition WHERE order_id = {orderId}");
            Update($"DELETE FROM ord WHERE order_id = {orderId}");

            Connection();
        }

        private string GetOrderType(long orderId)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            SqlCommand command = new SqlCommand($"SELECT order_type FROM ord WHERE order_id = {orderId}", sqlConnection);

            return (string)command.ExecuteScalar();
        }

        private Dictionary<long, int> GetOrderCompositionProducts(long orderId)
        {

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            Dictionary<long, int> products = new Dictionary<long, int>();

            SqlCommand command = new SqlCommand($"SELECT product_id, quantity FROM order_composition WHERE order_id = {orderId}", sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long productId = (long)reader["product_id"];
                int quantity = (int)reader["quantity"];
                products.Add(productId, quantity);
            }

            reader.Close();

            Connection();

            return products;
        }

        public void DeleteSupplier(DataRowView selectedRow)
        {
            Update($"DELETE FROM Supplier Where supplier_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void DeleteProduct(DataRowView selectedRow)
        {
            Update($"DELETE FROM Product Where product_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void DeleteProductType(DataRowView selectedRow)
        {
            Update($"DELETE FROM Product_type Where product_type_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadProductType(DataGrid grid)
        {
            Select(selectProductType, grid);
        }

        public void ReadProductTypeToComboBox(ComboBox box)
        {
            ComboBoxToTable(selectProductType, box);
        }

        public void ReadProductToComboBox(ComboBox box)
        {
            ComboBoxToTable(selectProductAll, box);
        }

        public void ReadSupplierToComboBox(ComboBox boxi)
        {
            ComboBoxToTable(selectSupplierAll, boxi);
        }

        public void ReadProduct(DataGrid grid)
        {
            Select(selectProduct, grid);
        }


        public DataTable GetOrdersWithProducts()
        {
            Connection();

            string selectOrderWithoutProducts = "SELECT DISTINCT ord.order_id, supplier.title, account.surname, ord.amount, ord.order_date, ord.order_type FROM ord, supplier, account, order_composition WHERE ord.supplier_id = supplier.supplier_id AND ord.account_id = account.account_id";

            DataTable orderTable = Select(selectOrderWithoutProducts);
            orderTable.Columns.Add("product", typeof(DataTable));

            foreach (DataRow row in orderTable.Rows)
            {
                long orderId = (long)row["order_id"];
                string query = $"SELECT product.title FROM product JOIN order_composition ON product.product_id = order_composition.product_id WHERE order_composition.order_id = {orderId}";

                DataTable productTable = Select(query);
                row["product"] = productTable;
            }
            Connection();

            return orderTable;
        }

        public void ReadAddFromComboBoxOrder(DataGrid grid)
        {
            Select(selectProductAll, grid);
        }

        public void ReadSupplier(DataGrid grid)
        {
            Select(selectSupplier, grid);
        }

        public void ComboBoxToTable(string query, ComboBox box)
        {
            Connection();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            box.Items.Clear();
            while (reader.Read())
            {
                ComboBoxDTO dto = new ComboBoxDTO();
                dto.id = reader.GetInt64(0);
                dto.name = reader.GetString(1);
                box.Items.Add(dto);
            }
            reader.Close();
            Connection();
        }
    }
}
