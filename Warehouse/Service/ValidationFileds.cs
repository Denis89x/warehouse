using System.Text.RegularExpressions;
using System.Windows;

namespace Warehouse.Service
{
    internal class ValidationFileds
    {
        const string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])[a-zA-Z\d]{6,16}$";
        const string patternUsername = @"^[a-zA-Z]{4,12}$";

        public bool ValidationRegistrationFields(string username, string firstPassword, string secondPassword)
        {
            if (!Regex.IsMatch(username, patternUsername))
            {
                MessageBox.Show("Username должен быть длинной от 4х и до 12 латинскими символами!");
                return false;
            }

            if (!firstPassword.Equals(secondPassword))
            {
                MessageBox.Show("Пароли должны совпадать!");
                return false;
            }

            if (!Regex.IsMatch(firstPassword, pattern))
            {
                MessageBox.Show("Минимальный размер пароля - 6, минимум 1 знак, минимум 1 заглавная буква и минимум 1 цифра, макимальный размер 16!");
                return false;
            }

            return true;
        }

        public bool ValidationPassword(string password)
        {
            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Минимальный размер пароля - 6, минимум 1 знак, минимум 1 заглавная буква и минимум 1 цифра, макимальный размер 16!");
                return false;
            }

            return true;
        }

        public bool ValidationAuth(string username)
        {
            if (!Regex.IsMatch(username, patternUsername))
            {
                MessageBox.Show("Username должен быть длинной от 4х и до 12 латинскими символами!");
                return false;
            }

            return true;
        }

        public string ReturnRole(bool isAdmin)
        {
            if (isAdmin)
                return "ROLE_ADMIN";
            else
                return "ROLE_USER";
        }
    }
}
