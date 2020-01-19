using System.Collections.Generic;
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
        [DataMember] private Dictionary<string, float> priceEvolution;
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
            priceEvolution = new Dictionary<string, float>();
        }

        ~Product()
        {
            try
            {
                File.Delete(imagePath);
            }
            catch (System.Exception)
            {
                // Nothing
            }
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
                if (imagePath != value && imagePath != "")
                {
                    File.Delete(imagePath);
                }
                imagePath = value;
            }
        }

        /// <summary>
        /// Liste de valeurs du prix du produit
        /// </summary>
        public Dictionary<string, float> PriceEvolution { get => priceEvolution; set => priceEvolution = value; }
    }
}
