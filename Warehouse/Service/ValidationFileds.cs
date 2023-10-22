using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;

namespace Warehouse.Service
{
    internal class ValidationFileds
    {
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

        public bool ValidationProductAdd(string title, string cost, string description, ComboBoxDTO box)
        {
            if (!ValidationProductTypeTitle(title))
                return false;

            if (!ValidationCost(cost))
                return false;

            if (!ValidationProductDescription(description))
                return false;

            if (!ValidationComboBoxItem(box))
                return false;

            return true;
        }

        public bool ValidationComboBoxItem(ComboBoxDTO combo)
        {
            if (combo == null)
            {
                MessageBox.Show("Выберите тип продукта!");
                return false;
            }

            return true;
        }

        public bool ValidationProductDescription(string description)
        {
            string pattern = @"^[a-zA-Zа-яА-Я\s]{3,60}$";

            if (!Regex.IsMatch(description, pattern))
            {
                MessageBox.Show("Размер описания от 3 до 60 символов, без цифр и знаков!");
                return false;
            }

            return true;
        }

        public double CastCostToDouble(string cost)
        {
            if (double.TryParse(cost, out double result))
                return result;

            return 0.0;
        }

        public bool ValidationCost(string cost)
        {
            string pattern = @"^\d+$";

            
            if (double.TryParse(cost, out double costDouble))
            {
                if (!Regex.IsMatch(costDouble.ToString(), pattern) || costDouble <= 0 || costDouble > 10000)
                {
                    MessageBox.Show("Число должно быть больше 0 и не больше 10000");
                    return false;
                }
            } else
            {
                MessageBox.Show("Введите число!");
                return false;
            }

            return true;
        }

        public bool ValidationProductTypeTitle(string title)
        {
            string productTypePattern = @"^[a-zA-Zа-яА-Я]{3,30}$";

            if (!Regex.IsMatch(title, productTypePattern))
            {
                MessageBox.Show("Размер наименования от 3 до 30 символов, без цифр и знаков!");
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
            string surnamePattern = @"^[a-zA-Zа-яА-Я]*-?[a-zA-Zа-яА-Я]{3,29}$";

            if (!Regex.IsMatch(surname, surnamePattern) || (surname.Length < 3) || (surname.Length > 30))
            {
                MessageBox.Show("Минимальный размер фамилии - 3, без знаков и цифр, максимальный размер 30! " + surname );
                return false;
            }

            return true;
        }

        public bool ValidationFirstName(string firstName)
        {
            string firstNamePattern = @"^[a-zA-Zа-яА-Я]*-?[a-zA-Zа-яА-Я]{3,24}$";

            if (!Regex.IsMatch(firstName, firstNamePattern) || (firstName.Length < 3) || (firstName.Length > 25))
            {
                MessageBox.Show("Минимальный размер имени - 3, без знаков и цифр, максимальный размер 25!");
                return false;
            }

            return true;
        }

        public bool ValidationMiddleName(string middleName)
        {
            string middleNamePattern = @"^[a-zA-Zа-яА-Я]*-?[a-zA-Zа-яА-Я]{3,29}$";

            if (!Regex.IsMatch(middleName, middleNamePattern) || (middleName.Length < 3) || (middleName.Length > 25))
            {
                MessageBox.Show("Минимальный размер отчества - 3, без символов и цифр, максимальный размер 30!");
                return false;
            }

            return true;
        }

        public bool ValidationPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,30}$";

            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Минимальный размер пароля - 6, минимум 1 заглавная буква и минимум 1 цифра, максимальный размер 30!");
                return false;
            }

            return true;
        }

        public bool ValidationAuth(string username)
        {
            string patternUsername = @"^[a-zA-Z]{4,12}$";

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
