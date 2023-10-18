using System.Windows;
using Warehouse.Storage;

namespace Warehouse.Auth
{
    public partial class AuthPage : Window
    {
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
                MessageBox.Show($"Auth: {AuthManager.CurrentUsername}");
            } else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
