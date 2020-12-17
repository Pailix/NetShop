using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetShop
{
    class Product
    {
        private static readonly ImageConverter _imageConverter = new ImageConverter();
        private byte[] imageArray;
        public int ProductID { get; private set; }
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public decimal Price { get; private set; }
        public BitmapSource Image
        {
            get
            {
                return GetImageFromByteArray();
            }
        }
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
            job spq
        }
        private BitmapSource GetImageFromByteArray()
        {
            return (BitmapSource)new ImageSourceConverter().ConvertFrom(ImageArray);
        }
    }
}
