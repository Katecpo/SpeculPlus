using Logic;
using System.ComponentModel;
using Xamarin.Forms;

namespace SpeculPlus
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product product;
        private CategoryViewModel category;

        /// <summary>
        /// Crée le vue-modèle d'un produit
        /// </summary>
        /// <param name="p">Le modèle du produit</param>
        public ProductViewModel(Product p)
        {
            product = p;
        }

        /// <summary>
        /// Le nom du produit
        /// </summary>
        public string Name
        {
            get => product.Name;
            set
            {
                product.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// Le prix du produit
        /// </summary>
        public float Price
        {
            get => product.Price;
            set
            {
                product.Price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }

        /// <summary>
        /// Le code-barre du produit
        /// </summary>
        public string Barcode
        {
            get => product.Barcode;
            set
            {
                product.Barcode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Barcode"));
            }
        }

        /// <summary>
        /// Le chemin de l'image du produit
        /// </summary>
        public string ImagePath
        {
            get => product.ImagePath;
            set
            {
                product.ImagePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImagePath"));
            }
        }

        /// <summary>
        /// L'image du produit
        /// </summary>
        public ImageSource ImageSource
        {
            get => product.ImageSource;
        }

        /// <summary>
        /// La catégorie du produit
        /// </summary>
        public CategoryViewModel Category
        {
            get => category;
            set
            {
                if (product.Category != null)
                {
                    category.Remove(this);
                }
                value.Add(this);

                product.Category = value.Category;
                category = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category"));
            }
        }

        /// <summary>
        /// La quantité du produit
        /// </summary>
        public int Quantity 
        { 
            get => product.Quantity;
            set
            {
                product.Quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        /// <summary>
        /// Le produit du vue-modèle
        /// </summary>
        public Product Product { get => product; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
