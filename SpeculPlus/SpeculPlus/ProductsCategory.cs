using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpeculPlus
{
    public class ProductsCategory : ObservableCollection<ProductViewModel>
    {
        #region Attributes
        private string name;
        private string color;
        #endregion

        #region Constructors
        /// <summary>
        /// Crée une catégorie de produits
        /// </summary>
        /// <param name="name">Le nom de la catégorie</param>
        /// <param name="listeProduit">La liste de produits</param>
        public ProductsCategory(CategoryViewModel parent, IEnumerable<ProductViewModel> listeProduit) : base(listeProduit)
        {
            Name = parent.Name;
            Color = parent.Color;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Couleur de la catégorie
        /// </summary>
        public string Color { get => color; set => color = value; }
        #endregion
    }
}
