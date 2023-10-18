using System;
using System.Windows;
using System.Security.Cryptography;
using System.Text;

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
                if (new Database().CountUsersWithLogin(username) == 0)
                {
                    string query = $"INSERT INTO Account (username, password, role) VALUES ('{username}', '{GetSHA256Hash(firstPassword)}', '{role}')";
                    new Database().Update(query);
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

        public string GetSHA256Hash(string input)
        {
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
