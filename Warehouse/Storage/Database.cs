using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse
{
    internal class Database
    {
        static string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=warehouse;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        SqlConnection sqlConnection = new SqlConnection(connection);
        private string selectProductType = $"select product_type_id, type_name from product_type";
        private string selectProduct = $"select product.product_id, product_type.type_name, product.presence, product.cost, product.description, product.title  from product, product_type WHERE product.product_type_id = product_type.product_type_id";

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

        public void Select(string query, DataGrid dataGrid)
        {
            Connection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            Connection();
        }

        public int CountUsersWithLogin(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}'", sqlConnection);
            int count = (int) command.ExecuteScalar();
            Connection();
            return count;
        }

        public bool Check(string username, string password)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}' AND Password='{PasswordEncoder.GetSHA256Hash(password)}'", sqlConnection);
            int result = (int) command.ExecuteScalar();
            Connection();

            return result > 0;
        }

        public bool CheckAdmin(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}' AND role='ROLE_ADMIN'", sqlConnection);
            int result = (int) command.ExecuteScalar();
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

        public void CreateProductType(string title)
        {
            Update($"insert into product_type (type_name) values (N'{title.ToLower()}')");
        }

        public void CreateProduct(string title, double cost, string description, ComboBoxDTO dto)
        {
            Update($"insert into product (product_type_id, presence, cost, description, title) values ('{dto.id}', '{0}', '{cost}', N'{description}', N'{title}')");
        }

        public void ReadProductType(DataGrid grid)
        {
            Select(selectProductType, grid);
        }

        public void ReadProductTypeToComboBox(ComboBox box)
        {
            ComboBoxToTable(selectProductType, box);
        }

        public void ReadProduct(DataGrid grid)
        {
            Select(selectProduct, grid);
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
