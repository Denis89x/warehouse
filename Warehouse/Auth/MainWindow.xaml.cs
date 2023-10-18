using System.Windows;
using Warehouse.Auth;
using Warehouse.Service;

namespace Warehouse
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string firstPassword = FirstPassword.Password;
            string secondPassword = SecondPassword.Password;
            bool isAdmin = (bool) AdminBox.IsChecked;
            string role = ReturnRole(isAdmin);

            if (firstPassword.Equals(secondPassword)) 
            {
                Database database = new Database();
                if (database.CountUsersWithLogin(username) == 0)
                {
                    string query = $"INSERT INTO Account (username, password, role) VALUES ('{username}', '{PasswordEncoder.GetSHA256Hash(firstPassword)}', '{role}')";
                    database.Update(query);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }
            } else
            {
                MessageBox.Show($"Пароли не совпадают!");
                return;
            }
        }

        private string ReturnRole(bool isAdmin)
        {
            if (isAdmin)
                return "ROLE_ADMIN";
            else
                return "ROLE_USER";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthPage auth = new AuthPage();
            auth.Show();
            this.Hide();
        }
    }
}
