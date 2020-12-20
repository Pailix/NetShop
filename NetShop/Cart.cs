using System.Windows.Media.Imaging;

namespace NetShop
{
    public class Cart
    {
        public int ProductID { get; private set; }
        public string OrderName { get; private set; }
        public decimal OrderPrice { get; private set; }
        public BitmapSource OrderImage { get; private set; }
        public int NumberOfProducts { get; private set; }
        public Cart(int id, string name, decimal price, BitmapSource image)
        {
            ProductID = id;
            OrderName = name;
            NumberOfProducts = 1;
            OrderPrice = price;
            OrderImage = image;
        }
        // общая сумма, если число товаров больше одного
        public decimal totalPrice = 1;
        public decimal TotalPrice
        {
            get
            {
                return OrderPrice * NumberOfProducts;
            }
        }
        // проверка на наличие продукта в корзине, если есть - инкремент
        public bool IsProductInCart()
        {
            if (NumberOfProducts >= 1)
            {
                NumberOfProducts++;
                return true;
            }
            else
            {
                return false;
            }
        }
        // декремент при удалении объекта из корзины
        public void DecrementProduct()
        {
            NumberOfProducts--;
        }
    }
}
