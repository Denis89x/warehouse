using System.Text.RegularExpressions;
using System.Windows;

namespace Warehouse.Service
{
    internal class ValidationFileds
    {
        const string pattern = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,30}$";
        const string patternUsername = @"^[a-zA-Z]{4,12}$";
        const string surnamePattern = @"^[a-zA-Zа-яА-Я]*-?[a-zA-Zа-яА-Я]{3,29}$";
        const string firstNamePattern = @"^[a-zA-Zа-яА-Я]*-?[a-zA-Zа-яА-Я]{3,24}$";
        const string middleNamePattern = @"^[a-zA-Zа-яА-Я]*-?[a-zA-Zа-яА-Я]{3,29}$";


        public bool ValidateFields(string username, string firstPassword, string secondPassword, string surname, string firstName, string middleName)
        {
            if (!ValidationAuth(username))
            {
                return false;
            }

            if (!ValidationTwoPasswords(firstPassword, secondPassword))
            {
                return false;
            }

            if (!ValidationPassword(firstPassword))
            {
                return false;
            }

            if (!ValidationSurname(surname))
            {
                return false;
            }

            if (!ValidationFirstName(firstName))
            {
                return false;
            }

            if (!ValidationMiddleName(middleName))
            {
                return false;
            }

            return true;
        }


        public bool ValidationTwoPasswords(string firstPassword, string secondPassword)
        {
            if (!firstPassword.Equals(secondPassword))
            {
                MessageBox.Show("Пароли должны совпадать!");
                return false;
            }

            return true;
        }

        public bool ValidationSurname(string surname)
        {
            if (!Regex.IsMatch(surname, surnamePattern) || (surname.Length < 3) || (surname.Length > 30))
            {
                MessageBox.Show("Минимальный размер фамилии - 3, без знаков и цифр, максимальный размер 30! " + surname );
                return false;
            }

            return true;
        }

        public bool ValidationFirstName(string firstName)
        {
            if (!Regex.IsMatch(firstName, firstNamePattern) || (firstName.Length < 3) || (firstName.Length > 25))
            {
                MessageBox.Show("Минимальный размер имени - 3, без знаков и цифр, максимальный размер 25!");
                return false;
            }

            return true;
        }

        public bool ValidationMiddleName(string middleName)
        {
            if (!Regex.IsMatch(middleName, middleNamePattern) || (middleName.Length < 3) || (middleName.Length > 25))
            {
                MessageBox.Show("Минимальный размер отчества - 3, без символов и цифр, максимальный размер 30!");
                return false;
            }

            return true;
        }

        public bool ValidationPassword(string password)
        {
            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Минимальный размер пароля - 6, минимум 1 заглавная буква и минимум 1 цифра, максимальный размер 30!");
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
