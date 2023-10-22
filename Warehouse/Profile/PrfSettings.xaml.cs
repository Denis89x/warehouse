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
                        database.UpdateUsername(newUsername);
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
                        database.UpdatePassword(newPassword);
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
                                database.UpdateUsernameAndPassword(newUsername, newPassword);
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

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
