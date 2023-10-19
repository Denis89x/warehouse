using System.Threading;
using System.Windows;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.Profile
{
    public partial class PrfSettings : Window
    {
        ValidationFileds validationFileds = new ValidationFileds();

        public PrfSettings()
        {
            InitializeComponent();
            RegistrationLabel.Content = AuthManager.CurrentUsername;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string newUsername = UsernameBox.Text;
            string currentPassword = CurrentPassword.Password;
            string newPassword = NewPassword.Password;

            if (newUsername.Length > 0 && currentPassword.Length <= 0)
            {
                if (validationFileds.ValidationAuth(newUsername))
                {
                    Database database = new Database();
                    if (database.CountUsersWithLogin(newUsername) == 0)
                    {
                        string query = $"UPDATE Account SET username = '{newUsername.ToLower()}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}'";
                        database.Update(query);
                        AuthManager.CurrentUsername = newUsername;
                        MessageBox.Show("Данные успешно обновлены!");
                        Thread.Sleep(300);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username должен быть уникальным!");
                    }
                }
            }

            if (newUsername.Length <= 0 && currentPassword.Length > 0)
            {
                if (validationFileds.ValidationPassword(newPassword))
                {
                    Database database = new Database();
                    if (database.Check(AuthManager.CurrentUsername, currentPassword))
                    {
                        string query = $"UPDATE Account SET password = '{PasswordEncoder.GetSHA256Hash(newPassword)}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}'";
                        database.Update(query);
                        MessageBox.Show("Данные успешно обновлены!");
                        Thread.Sleep(300);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный текущий пароль!");
                    }
                }
            }

            if (newUsername.Length > 0 && currentPassword.Length > 0)
            {
                if (validationFileds.ValidationAuth(newUsername))
                {
                    Database database = new Database();
                    if (database.CountUsersWithLogin(newUsername) == 0)
                    {
                        if (validationFileds.ValidationPassword(newPassword))
                        {
                            if (database.Check(AuthManager.CurrentUsername, currentPassword))
                            {
                                string query = $"UPDATE Account SET username = '{newUsername.ToLower()}', password = '{newPassword}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}' AND password = '{PasswordEncoder.GetSHA256Hash(currentPassword)}'"; 
                                database.Update(query);
                                AuthManager.CurrentUsername = newUsername;
                                MessageBox.Show("Данные успешно обновлены!");
                                Thread.Sleep(300);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный текущий пароль!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username должен быть уникальным!");
                    }
                }
            }

            
        }

        //string query = $"UPDATE Account SET username = '{newUsername.ToLower()}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}' AND password = '{PasswordEncoder.GetSHA256Hash(currentPassword)}'";


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
