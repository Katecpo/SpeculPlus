using Logic;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts.Forms;

namespace SpeculPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
        private ProductViewModel p = null;
        private IProductStorage storage;
        private CategoryViewModel cvm = null;

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
            Title = "Édition";

            //var chart = new RadarChart();
        }

        /// <summary>
        /// Modifie un produit qui n'est pas encore dans la liste de produit
        /// </summary>
        /// <param name="p">Le vue/modèle du produit à modifer</param>
        /// <param name="storage">Le stockage à utiliser</param>
        /// <param name="clvm">Le vue/modèle de la liste de catégories à laquelle ajouter le produit</param>
        public EditPage(ProductViewModel p, IProductStorage storage, CategoryViewModel cvm)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;
            this.cvm = cvm;
        }

        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            if (cvm != null)
            {
                cvm.Add(p.Product);
                storage.Save();
            }

            await Navigation.PopToRootAsync();

        }

    }
}