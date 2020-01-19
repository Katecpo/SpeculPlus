using System.IO;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace Logic
{
    [DataContract]
    /// <summary>
    /// Produit
    /// </summary>
    public class Product
    {
        [DataMember] private string name;
        [DataMember] private float price;
        [DataMember] private string imagePath;
        [DataMember] private string barcode;
        [DataMember] private int quantity;
        private Category category;

        /// <summary>
        /// Crée un produit vierge
        /// </summary>
        public Product()
        {
            name = "";
            price = 0f;
            imagePath = "";
            barcode = "";
            category = null;
            quantity = 1;
        }

        ~Product()
        {
            File.Delete(imagePath);
        }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Prix du produit
        /// </summary>
        public float Price
        {
            get => price;
            set => price = value;
        }

        /// <summary>
        /// Code-barre du produit
        /// </summary>
        public string Barcode
        {
            get => barcode;
            set => barcode = value;
        }

        /// <summary>
        /// Le chemin vers l'image du produit
        /// </summary>
        public ImageSource ImageSource
        {
            get 
            {
                if (ImagePath != "")
                    return ImageSource.FromFile(imagePath);

                return "barcode";
            }
        }

        /// <summary>
        /// La catégorie du produit
        /// </summary>
        public Category Category
        {
            get => category;
            set => category = value;
        }

        /// <summary>
        /// La quantité du produit
        /// </summary>
        public int Quantity { get => quantity; set => quantity = value; }
        public string ImagePath { 
            get => imagePath; 
            set
            {
                if (imagePath != value)
                {
                    File.Delete(imagePath);
                    imagePath = value;
                }
            }
        }
    }
}
