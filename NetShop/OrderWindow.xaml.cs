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
        private static readonly SqlConnection orderTableConnection =
            new SqlConnection(@"Data Source=HOME-PC;Initial Catalog=OrdersDataBase;Integrated Security=True");
        // создано для передачи списка товаров в корзине из главной формы в подчиненную
        private List<Cart> cartList;
        PersonalInformation Bobby;
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
            Bobby = new PersonalInformation("", "", "", "", "", "");
            DataContext = Bobby;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseController.InsertOrder(new PersonalInformation(SurnameTextBox.Text, NameTextBox.Text, PatronymicTextBox.Text, AddressTextBox.Text,
            PhoneTextBox.Text, MailTextBox.Text), orderTableConnection, cartList);
            MessageBox.Show("Ваш заказ отправлен на оформление.");
        }
        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
            FinishButton.IsEnabled = false;         
        }

        private void AllTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FinishButton.IsEnabled = true;
        }
    }
}
