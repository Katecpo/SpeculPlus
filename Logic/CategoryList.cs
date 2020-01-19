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
        [DataMember] private List<Category> categories;
        [DataMember] private Category defaultCategory;

        /// <summary>
        /// Crée une liste de catégories de produits
        /// </summary>
        public CategoryList()
        {
            categories = new List<Category>();
        }

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
        /// La liste des catégories
        /// </summary>
        public List<Category> Categories { get => categories; }

        /// <summary>
        /// La catégorie par défaut à laquelle sont ajoutés les nouveaux produits
        /// </summary>
        public Category DefaultCategory { get => defaultCategory; set => defaultCategory = value; }
    }
}
