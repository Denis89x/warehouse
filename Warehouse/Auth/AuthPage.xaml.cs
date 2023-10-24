using System.Windows;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.AddPage;
using Warehouse.View.Main;

namespace Warehouse.Auth
{
    public partial class AuthPage : Window
    {
        ValidationFileds validationFileds = new ValidationFileds();

        public AuthPage()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = Password.Password;

            Database database = new Database();
            bool isAuth = database.Check(username, password);

            if (isAuth)
            {
                AuthManager.CurrentUsername = username;
                MainPage mainPage = new MainPage();
                this.Hide();
                mainPage.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
