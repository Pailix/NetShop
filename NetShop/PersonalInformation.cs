using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetShop
{
    class PersonalInformation
    {
        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string Patronymic { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public string MailAddress { get; private set; }
        
        public PersonalInformation(string surname, string name, string patronymic, string address, string phoneNumber, string mailAddress)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Address = address;
            PhoneNumber = phoneNumber;
            MailAddress = mailAddress;
        }
    }
}
