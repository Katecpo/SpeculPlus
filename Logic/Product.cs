using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    /// <summary>
    /// Produit
    /// </summary>
    public class Product
    {
        [DataMember] private Category category;
        [DataMember] private string name;
        [DataMember] private float price;
        [DataMember] private string image;
        [DataMember] private string barcode;

        /// <summary>
        /// Crée un produit vierge
        /// </summary>
        public Product()
        {
            category = null;
            name = "";
            price = 0f;
            image = "";
            barcode = "";
        }

        /// <summary>
        /// Catégorie du produit
        /// </summary>
        public Category Category
        {
            get { return category; }
            set { 
                category = value;
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
        /// L'image du produit
        /// </summary>
        public string Image
        {
            get => image;
            set => image = value;
        }
    }
}
