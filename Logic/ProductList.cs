using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    /// <summary>
    /// Liste de produits
    /// </summary>
    public class ProductList 
    {
        [DataMember] private List<Product> products = new List<Product>();

        /// <summary>
        /// Ajoute un produit dans la liste de produits
        /// </summary>
        /// <param name="p">Produit à ajouter</param>
        public void Add(Product p)
        {
            products.Add(p);
        }

        /// <summary>
        /// Supprimer un produit dans la liste de produits
        /// </summary>
        /// <param name="p">Produit à supprimer</param>
        public void Remove(Product p)
        {
            products.Remove(p);
        }

        /// <summary>
        /// Liste de produits
        /// </summary>
        public List<Product> Products
        {
            get => products;
        }
    }
}
