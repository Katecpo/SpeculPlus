using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class Category
    {
        [DataMember] private string name;
        [DataMember] private string color;
        [DataMember] private List<Product> products;

        /// <summary>
        /// Crée une catégorie de produits
        /// </summary>
        /// <param name="name">Le nom de la catégorie</param>
        /// <param name="color">La couleur de la catégorie</param>
        public Category(string name, string color)
        {
            products = new List<Product>();
            this.name = name;
            this.color = color;

            foreach (var p in products)
                p.Category = this;
        }

        /// <summary>
        /// Le nom de la catégorie
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// La couleur de la catégorie
        /// </summary>
        public string Color { get => color; set => color = value; }

        /// <summary>
        /// La liste de produits de la catégorie
        /// </summary>
        public List<Product> Products { get => products; }

        /// <summary>
        /// Ajoute un produit dans la catégorie
        /// </summary>
        /// <param name="p">Produit à ajouter</param>
        public void Add(Product p)
        {
            products.Add(p);
        }

        /// <summary>
        /// Supprime un produit de la catégorie
        /// </summary>
        /// <param name="p">Produit à supprimer</param>
        public void Remove(Product p)
        {
            products.Remove(p);
        }
    }
}
