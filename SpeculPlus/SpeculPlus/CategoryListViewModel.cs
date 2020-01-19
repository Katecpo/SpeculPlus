using Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpeculPlus
{
    /// <summary>
    /// Vue modèle d'une liste de catégories de produits
    /// </summary>
    public class CategoryListViewModel
    {
        #region Attributes
        private ObservableCollection<CategoryViewModel> categoriesVm;
        private CategoryList categoryList;
        private ObservableCollection<ProductsCategory> productsCategory;
        #endregion

        #region Constructors
        /// <summary>
        /// Crée le vue-modèle d'une liste de catégories
        /// </summary>
        /// <param name="categoryList">La liste de catégories</param>
        public CategoryListViewModel(CategoryList categoryList)
        {
            this.categoryList = categoryList;
            categoriesVm = new ObservableCollection<CategoryViewModel>();
            productsCategory = new ObservableCollection<ProductsCategory>();

            // Crée les vue-modèles à partir des catégories
            foreach (Category c in categoryList.Categories)
                categoriesVm.Add(new CategoryViewModel(c));

            // Regrouper toutes les catégories contenant les produits
            foreach (CategoryViewModel cvm in categoriesVm)
                productsCategory.Add(cvm.Products);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Liste de vues-modèles des catégories
        /// </summary>
        public ObservableCollection<CategoryViewModel> Categories { get => categoriesVm; }

        /// <summary>
        /// La catégorie par défaut de la liste
        /// </summary>
        public CategoryViewModel DefaultCategory
        {
            get
            {
                return categoriesVm.Single(i => i.Name == categoryList.DefaultCategory);
            }
            set
            {
                categoryList.DefaultCategory = value.Name;
            }
        }

        /// <summary>
        /// La liste des catégories contenant les produits
        /// </summary>
        public ObservableCollection<ProductsCategory> ProductsCategory { get => productsCategory; }
        #endregion

        #region Methods
        /// <summary>
        /// Ajoute une categorie dans la liste de categories
        /// </summary>
        /// <param name="c">Catégorie à ajouter</param>
        public void Add(Category c)
        {
            categoryList.Add(c);

            CategoryViewModel cvm = new CategoryViewModel(c);
            categoriesVm.Add(cvm);
            productsCategory.Add(cvm.Products);
        }

        /// <summary>
        /// Supprime une catégorie dans la liste de categories
        /// </summary>
        /// <param name="c">Catégorie vm à supprimer</param>
        public void Remove(CategoryViewModel c)
        {
            categoryList.Remove(c.Category);
            categoriesVm.Remove(c);
            productsCategory.Remove(c.Products);
        }

        /// <summary>
        /// Retourne tous les produits de toutes les catégories
        /// </summary>
        /// <returns></returns>
        public List<ProductViewModel> GetAllProducts()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();

            foreach (var c in categoriesVm)
                foreach (var p in c.Products)
                    result.Add(p);

            return result;
        }

        /// <summary>
        /// Crée les catégories par défaut si aucune catégorie n'est disponible
        /// </summary>
        public void CreateDefaultCategories()
        {
            if (Categories.Count == 0)
            {
                Category c = new Category("Nouvelle catégorie", "Black");
                Add(c);
                DefaultCategory = categoriesVm[0];
            }
        }
        #endregion
    }
}
