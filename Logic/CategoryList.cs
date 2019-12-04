using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    /// <summary>
    /// Liste de catégories
    /// </summary>
    public class CategoryList
    {
        [DataMember] private List<Category> categories = new List<Category>();

        /// <summary>
        /// Ajoute une categorie dans la liste de categories
        /// </summary>
        /// <param name="c">Catégorie à ajouter</param>
         
        public void Add(Category c)
        {
            categories.Add(c);
        }

        /// <summary>
        /// Supprime une catégorie dans la liste de categories
        /// </summary>
        /// <param name="c">Catégorie à supprimer</param>
        public void Remove(Category c)
        {
            categories.Remove(c);
        }

        /// <summary>
        /// Renvoie la liste de catégories
        /// </summary>
        /// <returns>Catégories</returns>
        public List<Category> GetAll()
        {
            return categories;
        }        
    }
}
