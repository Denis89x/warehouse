using System.Windows;
using Warehouse.Auth;
using Warehouse.Service;

namespace Warehouse
{
    public partial class MainWindow : Window
    {
        ValidationFileds validationFileds = new ValidationFileds();

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
            string role = validationFileds.ReturnRole(isAdmin);

            if (validationFileds.ValidationRegistrationFields(username.ToLower(), firstPassword, secondPassword)) 
            {
                Database database = new Database();
                if (database.CountUsersWithLogin(username) == 0)
                {
                    string query = $"INSERT INTO Account (username, password, role) VALUES ('{username.ToLower()}', '{PasswordEncoder.GetSHA256Hash(firstPassword)}', '{role}')";
                    database.Update(query);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthPage auth = new AuthPage();
            auth.Show();
            this.Hide();
        }
    }
}
