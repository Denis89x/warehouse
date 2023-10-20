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
            string surname = SecondName.Text;
            string firstName = FirstName.Text;
            string middleName = MiddleName.Text;
            bool isAdmin = (bool) AdminBox.IsChecked;
            string role = validationFileds.ReturnRole(isAdmin);

            if (validationFileds.ValidateFields(username, firstPassword, secondPassword, surname, firstName, middleName))
            {
                Database database = new Database();
                if (database.CountUsersWithLogin(username) == 0)
                {
                    string query = $"INSERT INTO Account (username, password, role, surname, first_name, middle_name) VALUES ('{username.ToLower()}', '{PasswordEncoder.GetSHA256Hash(firstPassword)}', '{role}', N'{surname.ToLower()}', N'{firstName.ToLower()}', N'{middleName.ToLower()}')";
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

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            UsernameBox.Visibility = Visibility.Collapsed;
            FirstPassword.Visibility = Visibility.Collapsed;
            SecondPassword.Visibility = Visibility.Collapsed;
            AdminBox.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;

            SecondName.Visibility = Visibility.Visible;
            FirstName.Visibility = Visibility.Visible;
            MiddleName.Visibility = Visibility.Visible;
            RegistrationButton.Visibility = Visibility.Visible;
            Back.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SecondName.Visibility = Visibility.Collapsed;
            FirstName.Visibility = Visibility.Collapsed;
            MiddleName.Visibility = Visibility.Collapsed;
            RegistrationButton.Visibility = Visibility.Collapsed;
            Back.Visibility = Visibility.Collapsed;

            UsernameBox.Visibility = Visibility.Visible;
            FirstPassword.Visibility = Visibility.Visible;
            SecondPassword.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            AdminBox.Visibility = Visibility.Visible;
        }
    }
}
