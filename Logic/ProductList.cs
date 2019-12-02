using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class ProductList
    {
        private List<Product> products = new List<Product>();

        /// <summary>
        /// Ajoute un produit dans la list de produits
        /// </summary>
        /// <param name="p"></param>
        public void Add(Product p)
        {
            products.Add(p);
        }

        /// <summary>
        /// Supprimer un produit dans la list de produits
        /// </summary>
        /// <param name="p"></param>
        public void Remove(Product p)
        {
            products.Remove(p);
        }

        /// <summary>
        /// Retourne la liste de produits
        /// </summary>
        /// <returns>products</returns>
        public List<Product> ListAll()
        {
            return products;
        }
    }
}
