using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace NetShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SqlConnection importTableConnection =
            new SqlConnection(@"Data Source=HOME-PC;Initial Catalog=ImportDataBase;Integrated Security=True");
        private List<Section<Product>> sectionsList = new List<Section<Product>>();
        private List<Cart> cartList = new List<Cart>();
        public MainWindow()
        {
            FillAll();
            InitializeComponent();
            ComboBoxInitialisation();
        }
        private void ComboBoxInitialisation()
        {
            SectionsComboBox.Items.Add("Все");
            SectionsComboBox.SelectedItem = "Все";
            foreach (var section in sectionsList)
            {
                SectionsComboBox.Items.Add(section.SectionName);
            }
        }
        private void FillAll()
        {
            FillSections();
            FillByProducts();
        }
        private void FillSections()
        {
            var sectionsTable = DataBaseController.LoadTable("SectionsTable", importTableConnection);
            var sectionIDs = sectionsTable.AsEnumerable().Select(ids => (int)ids["SectionID"]).ToList();
            var sectionNames = sectionsTable.AsEnumerable().Select(names => (string)names["SectionName"]).ToList();
            for (int i = 0; i < sectionsTable.Rows.Count; i++)
            {
                sectionsList.Add(new Section<Product>(sectionIDs[i], sectionNames[i].Trim()));
            }
        }
        private void FillByProducts()
        {
            var productsTable = DataBaseController.LoadTable("ProductsTable", importTableConnection);
            var linksTable = DataBaseController.LoadTable("ProductSectionLinkTable", importTableConnection);
            var productsIDs = productsTable.AsEnumerable().Select(ids => (int)ids["ProductID"]).ToList();
            for (int i = 0; i < sectionsList.Count; i++)
            {
                var products = from firstProductID in productsTable.AsEnumerable()
                               join linkProductID in linksTable.AsEnumerable()
                               on firstProductID.Field<int>("ProductID")
                               equals linkProductID.Field<int>("ProductID")
                               where linkProductID.Field<int>("SectionID") == i + 1
                               select firstProductID;
                AddProducts(i, products);
            }
        }
        private void AddProducts(int sectionID, IEnumerable<DataRow> products)
        {
            foreach (var obj in products)
            {
                sectionsList[sectionID].AddProduct(new Product(obj.Field<int>("ProductID"),
                    obj.Field<string>("Name"),
                    obj.Field<int>("Weight"),
                    Math.Round(obj.Field<decimal>("Price"), 2),
                    obj.Field<byte[]>("Image"),
                    obj.Field<object>("DateOfProduction").ToString().Substring(0, 10),
                    obj.Field<string>("ShelfLife")));
            }
        }
        private void BuyClick(object sender, RoutedEventArgs e)
        {
            var chosenProduct = ((Button)sender).DataContext as Product;
            foreach (var obj in cartList)
            {
                if (obj.OrderName == chosenProduct.Name)
                {
                    obj.IsProductInCart();
                    cart.ItemsSource = from product in cartList
                                       select product;
                    FinalSumTextBox.Text = CountFinalPrice() + " рублей";
                    return;
                }
            }
            cartList.Add(new Cart(chosenProduct.ProductID, chosenProduct.Name, chosenProduct.Price, chosenProduct.Image));        
            cart.ItemsSource = from product in cartList
                               select product;
            FinalSumTextBox.Text = CountFinalPrice() + " рублей";
        }
        private void RemoveFromCart(object sender, RoutedEventArgs e)
        {
            var chosenProduct = ((Button)sender).DataContext as Cart;
            for (int i=0; i < cartList.Count; i++)
            {
                if (cartList[i].ProductID==chosenProduct.ProductID && cartList[i].NumberOfProducts > 1)
                {
                    cartList[i].DecrementProduct();
                }
                else
                {
                    cartList.RemoveAt(i);
                }
            }
            cart.ItemsSource = from product in cartList
                               select product;
            FinalSumTextBox.Text = CountFinalPrice() + " рублей";
        }
        private int CountFinalPrice()
        {
            return (int)(from product in cartList select product.TotalPrice).Sum();
        }
        private void SectionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SectionsComboBox.SelectedItem.ToString() == "Все")
            {
                productsListView.ItemsSource = from section in sectionsList
                                               from items in section
                                               select items;
            }
            else
            {
                productsListView.ItemsSource = from section in sectionsList
                                               where section.SectionName == SectionsComboBox.SelectedItem.ToString()
                                               from items in section
                                               select items;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs args)
        {
            if (SearchTextBox.Text.Length != 0)
            {
                productsListView.ItemsSource = from section in sectionsList
                                               from items in section
                                               where items.Name.Contains(SearchTextBox.Text)
                                               select items;
            }
            else
            {
                productsListView.ItemsSource = from section in sectionsList
                                               from items in section
                                               select items;
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (CountFinalPrice() > 1000)
            {
                OrderWindow ordWin = new OrderWindow();
                Visibility = Visibility.Collapsed;
                ordWin.CartList = cartList;
                ordWin.ShowDialog();
                Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Сумма заказа менее 1000 рублей.");
            }
        }

        private void ClearCartButton_Click(object sender, RoutedEventArgs e)
        {
            cartList.Clear();
            cart.ItemsSource = null;
        }
    }
}
