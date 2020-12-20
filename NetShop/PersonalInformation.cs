using System;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace NetShop
{
    public class PersonalInformation : IDataErrorInfo
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        
        public PersonalInformation(string surname, string name, string patronymic, string address, string phoneNumber, string mailAddress)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Address = address;
            PhoneNumber = phoneNumber;
            MailAddress = mailAddress;
        }
        public PersonalInformation()
        {

        }
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Surname":
                        if (!Regex.IsMatch(Surname, @"^[a-zA-Z]+$", RegexOptions.CultureInvariant))
                        {
                            error = "Ошибка ввода фамилии!";
                        }
                        break;
                    case "Name":
                        if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$", RegexOptions.CultureInvariant))
                        {
                            error = "Ошибка ввода имени!";
                        }
                        break;
                    case "Patronymic":
                        if (!Regex.IsMatch(Patronymic, @"^[a-zA-Z]+$", RegexOptions.CultureInvariant))
                        {
                            error = "Ошибка ввода отчества!";
                        }
                        break;
                    case "Address":
                        if (!Regex.IsMatch(Address, @"^[a-zA-Z0-9]+$", RegexOptions.CultureInvariant))
                        {
                            error = "Ошибка ввода адреса!";
                        }
                        break;
                    case "PhoneNumber":
                        if (!Regex.IsMatch(PhoneNumber, @"^[0-9]+$", RegexOptions.CultureInvariant))
                        {
                            error = "Ошибка ввода номера телефона!";
                        }
                        break;
                    case "MailAdress":
                        if (!Regex.IsMatch(MailAddress, @"^[a-zA-Z0-9@]+$", RegexOptions.CultureInvariant))
                        {
                            error = "Ошибка ввода почты!";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
