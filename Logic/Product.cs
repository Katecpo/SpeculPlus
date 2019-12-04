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
        /// Catégorie du produit
        /// </summary>
        public Category Category
        {
            get => category;
            set => category = value;
        }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
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
    }
}
