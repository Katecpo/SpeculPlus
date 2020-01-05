using Logic;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;

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
        public EditPage(ProductViewModel p, IProductStorage storage, CategoryListViewModel clvm)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;

            listCat.ItemsSource = clvm.Categories;
            listCat.SelectedItem = p.Category;

            // Charts
            Microcharts.Entry[] entries = new Microcharts.Entry[5];
            for (int i = 0; i < 5; i++)
            {
                entries[i] = new Microcharts.Entry(i)
                {
                    Label = "0" + i + "/02/2020",
                    ValueLabel = "14",
                    Color = SKColor.Parse("#266489")
                };
            }

            var chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Spline,
                PointMode = PointMode.Square
            };
            priceChart.Chart = chart;
        }

        /// <summary>
        /// Modifie un produit qui n'est pas encore dans la liste de produit
        /// </summary>
        /// <param name="p">Le vue/modèle du produit à modifer</param>
        /// <param name="storage">Le stockage à utiliser</param>
        /// <param name="clvm">Le vue/modèle de la liste de catégories à laquelle ajouter le produit</param>
        public EditPage(ProductViewModel p, IProductStorage storage, CategoryViewModel cvm, CategoryListViewModel clvm)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;
            this.cvm = cvm;

            listCat.ItemsSource = clvm.Categories;
            listCat.SelectedItem = p.Category;
        }

        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            if (cvm != null)
            {
                cvm.Add(p.Product);
            }

            p.Category = listCat.SelectedItem as CategoryViewModel;
            storage.Save();

            await Navigation.PopToRootAsync();
        }

    }
}