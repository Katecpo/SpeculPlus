using Logic;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpeculPlus
{
    /// <summary>
    /// Vue modèle d'une catégorie
    /// </summary>
    public class CategoryViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private Category category;
        private ProductsCategory products;
        #endregion

        #region Constructors
        /// <summary>
        /// Crée le vue-modèle d'une catégorie et crée sa liste de produits correspondante
        /// </summary>
        /// <param name="category">La catégorie</param>
        public CategoryViewModel(Category category)
        {
            this.category = category;

            List<ProductViewModel> productsL = new List<ProductViewModel>();

            foreach (Product p in category.Products)
            {
                ProductViewModel productvm = new ProductViewModel(p);
                productsL.Add(productvm);
                productvm.Category = this;
            }

            products = new ProductsCategory(this, productsL);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        public string Name { 
            get => category.Name;
            set
            {
                category.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// Couleur de la catégorie
        /// </summary>
        public string Color { 
            get => category.Color; 
            set
            { 
                category.Color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
            }
        }

        /// <summary>
        /// La catégorie du vue-modèle
        /// </summary>
        public Category Category { get => category; }

        /// <summary>
        /// La catégorie qui contient tous les produits
        /// </summary>
        public ProductsCategory Products { get => products; }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Ajoute un produit dans la catégorie
        /// </summary>
        /// <param name="p">Produit à ajouter</param>
        public void Add(Product p)
        {
            category.Add(p);
            products.Add(new ProductViewModel(p));
        }

        /// <summary>
        /// Supprime un produit de la catégorie
        /// </summary>
        /// <param name="pvm">Vue-modèle de produit à supprimer</param>
        public void Remove(ProductViewModel pvm)
        {
            category.Remove(pvm.Product);
            products.Remove(pvm);
        }
        #endregion
    }
}