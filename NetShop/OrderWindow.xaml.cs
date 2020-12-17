using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetShop
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private static SqlConnection orderTableConnection =
            new SqlConnection(@"Data Source=HOME-PC;Initial Catalog=OrdersDataBase;Integrated Security=True");
        private List<Cart> cartList;
        public List<Cart> CartList
        {
            get { return cartList; }
            set
            {
                cartList = value;
            }
        }
        public OrderWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            if ((SurnameTextBox.Text.Length != 0) && (NameTextBox.Text.Length != 0) && (PatronymicTextBox.Text.Length != 0) &&
                (AddressTextBox.Text.Length != 0) && (PhoneTextBox.Text.Length != 0) && (MailTextBox.Text.Length != 0) &&
                (MailTextBox.Text.Contains("@")) && (PhoneTextBox.Text.All(Char.IsDigit)))
            {
                DataBaseController.InsertOrder(new PersonalInformation(SurnameTextBox.Text, NameTextBox.Text, PatronymicTextBox.Text, AddressTextBox.Text,
                    PhoneTextBox.Text, MailTextBox.Text), orderTableConnection, cartList);
                MessageBox.Show("Ваш заказ отправлен на оформление.");
            }
            else
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }
    }
}
