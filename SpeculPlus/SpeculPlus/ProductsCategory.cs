using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpeculPlus
{
    public class ProductsCategory : ObservableCollection<ProductViewModel>, INotifyPropertyChanged
    {
        #region Attributes
        private string name;
        private string color;
        private int productCount;
        private bool _expanded;
        private CategoryViewModel parent;
        #endregion

        #region Constructors
        /// <summary>
        /// Crée une catégorie de produits
        /// </summary>
        /// <param name="name">Le nom de la catégorie</param>
        /// <param name="listeProduit">La liste de produits</param>
        public ProductsCategory(CategoryViewModel parent, IEnumerable<ProductViewModel> listeProduit, bool expanded = true) : base(listeProduit)
        {
            this.parent = parent;
            Name = parent.Name;
            Color = parent.Color;
            Expanded = expanded;
            ProductCount = Count;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Nom de la catégorie formaté avec le nombre d'items de celle-ci
        /// </summary>
        public string NameWithItemCount
        {
            get
            {
                return string.Format("{0} ({1})", Name, ProductCount);
            }
        }

        /// <summary>
        /// Couleur de la catégorie
        /// </summary>
        public string Color { get => color; set => color = value; }

        /// <summary>
        /// État d'extension de la catégorie
        /// </summary>
        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Expanded"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StateIcon"));
                }
            }
        }

        /// <summary>
        /// Icône pour étendre la catégorie
        /// </summary>
        public string StateIcon
        {
            get => Expanded ? "leftarrow" : "rightarrow";
        }

        /// <summary>
        /// La catégorie à laquelle appartient la liste de produits
        /// </summary>
        public CategoryViewModel Parent { get => parent; }

        /// <summary>
        /// Le nombre de produits réels dans la catégorie
        /// </summary>
        public int ProductCount { get => productCount; set => productCount = value; }

        public new event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
