using Logic;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeculPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
        private ProductViewModel p = null;
        private IProductStorage storage;
        private ProductListViewModel plvm = null;

        /// <summary>
        /// A ne pas utiliser pour afficher la page
        /// </summary>
		public EditPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// Éditer un produit déjà présent dans la liste de produits
        /// </summary>
        /// <param name="p">Le vue/modèle du produit à modifer</param>
        /// <param name="storage">Le stockage à utiliser</param>
        public EditPage(ProductViewModel p, IProductStorage storage)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;
        }

        /// <summary>
        /// Modifie un produit qui n'est pas encore dans la liste de produit
        /// </summary>
        /// <param name="p">Le vue/modèle du produit à modifer</param>
        /// <param name="storage">Le stockage à utiliser</param>
        /// <param name="plvm">Le vue/modèle de la liste de produit à laquelle ajouter le produit</param>
        public EditPage(ProductViewModel p, IProductStorage storage, ProductListViewModel plvm)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;
            this.plvm = plvm;

            p.Image = "barcode";
        }

        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            //p.Name = name.Text;
            //p.Price = float.Parse(price.Text, CultureInfo.InvariantCulture);
            //p.Category = (Category)listCat.SelectedItem;

            await Navigation.PopToRootAsync();

            if (plvm != null)
            {
                plvm.Add(p.Product);
                storage.Update(p.Product);
            }
        }

    }
}