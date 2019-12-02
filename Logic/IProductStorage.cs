using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public interface IProductStorage
    {
        /// <summary>
        /// Créer un produit
        /// </summary>
        /// <returns></returns>
        Product Create();

        /// <summary>
        /// Update le produit
        /// </summary>
        /// <param name="p"></param>
        void Update(Product p);

        /// <summary>
        /// Supprime le produit
        /// </summary>
        /// <param name="p"></param>
        void Delete(Product p);

        /// <summary>
        /// Charge une liste de produits
        /// </summary>
        /// <returns></returns>
        List<Product> Load();
    }
}
