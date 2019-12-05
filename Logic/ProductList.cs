using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    /// <summary>
    /// Liste de produits
    /// </summary>
    public class ProductList 
    {
        [DataMember] private ObservableCollection<Product> products = new ObservableCollection<Product>();

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
        public ObservableCollection<Product> Products
        {
            get => products;
        }
    }
}
