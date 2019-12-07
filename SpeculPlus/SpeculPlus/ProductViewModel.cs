using Logic;
using System.ComponentModel;

namespace SpeculPlus
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product product;

        public ProductViewModel(Product p)
        {
            product = p;
        }

        public string Name
        {
            get => product.Name;
            set
            {
                product.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public float Price
        {
            get => product.Price;
            set
            {
                product.Price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }

        public string Barcode
        {
            get => product.Barcode;
            set
            {
                product.Barcode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Barcode"));
            }
        }

        public string Image
        {
            get => product.Image;
            set
            {
                product.Image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
            }
        }

        public Product Product { get => product; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
