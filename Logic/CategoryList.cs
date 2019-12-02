using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class CategoryList
    {
        private List<Category> categories = new List<Category>();

        /// <summary>
        /// Ajoute une categorie dans la liste de categories
        /// </summary>
        /// <param name="c"></param>
         
        public void Add(Category c)
        {
            categories.Add(c);
        }

        /// <summary>
        /// Supprime une categorie dans la liste de categories
        /// </summary>
        /// <param name="c"></param>
        public void Remove(Category c)
        {
            categories.Remove(c);
        }

        /// <summary>
        /// Renvoie la list de categories
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAll()
        {
            return categories;
        }
        
    }
}
