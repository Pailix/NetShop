using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetShop
{
    class Product
    {
        public int ProductID { get; private set; }
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public decimal Price { get; private set; }
        private byte[] imageArray;
        public byte[] ImageArray
        {
            get
            {
                return imageArray;
            }
            set
            {
                imageArray = value;
            }
        }
        public string DateOfProduction { get; private set; }
        public string ShelfLife { get; private set; }
        public Product (int id, string name, int weight, decimal price, byte[] imageArr, string date, string shelf)
        {
            ProductID = id;
            Name = name;
            Weight = weight;
            Price = price;
            ImageArray = imageArr;
            DateOfProduction = date;
            ShelfLife = shelf;
        }
        // конвертация одномерного байтового массива в bitmapsource => изображение на форме
        private BitmapSource GetImageFromByteArray()
        {
            return (BitmapSource)new ImageSourceConverter().ConvertFrom(ImageArray);
        }
        public BitmapSource Image
        {
            get
            {
                return GetImageFromByteArray();
            }
        }
    }
}
