using Logic;
using System;
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
        /// Éditer un produit déjà présent dans la liste de produits
        /// </summary>
        /// <param name="p">Le vue/modèle du produit à modifer</param>
        /// <param name="storage">Le stockage à utiliser</param>
        /// <param name="clvm">La liste de catégories</param>
        /// <param name="cvm">La catégorie à laquelle ajouter le produit</param>
        public EditPage(ProductViewModel p, IProductStorage storage, CategoryListViewModel clvm, CategoryViewModel cvm = null)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;

            listCat.ItemsSource = clvm.Categories;
            listCat.SelectedItem = p.Category;

            if (cvm != null)
                this.cvm = cvm;

            DrawChart();
        }

        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            if (cvm != null)
            {
                cvm.Add(p);
            }

            p.Category = listCat.SelectedItem as CategoryViewModel;

            // Si la quantité a été portée à 0 alors le produit est supprimé
            if (p.Quantity == 0)
            {
                p.Category.Remove(p);
            }

            storage.Save();

            await Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Dessine le graphique
        /// </summary>
        private void DrawChart()
        {
            // Charts
            Microcharts.Entry[] entries = new Microcharts.Entry[5];
            for (int i = 0; i < 5; i++)
            {
                var rndm = new Random().Next(5, 30);

                float avg = 0;
                int length = 0;
                foreach (var j in entries)
                    if (j != null)
                    {
                        length += 1;
                        avg += j.Value;
                    }

                avg /= length;

                SKColor color;
                if (rndm > avg * 1.25) // Bon
                {
                    color = SKColor.Parse("#196b3f");
                }
                else if (rndm > avg * 1.75) // Très bon
                {
                    color = SKColor.Parse("#00d964");
                }
                else if (rndm < avg * 0.75) // Mauvais
                {
                    color = SKColor.Parse("#d96500");
                }
                else if (rndm < avg * 0.25) // Très mauvais
                {
                    color = SKColor.Parse("#991700");
                }
                else // Moyenne
                {
                    color = SKColor.Parse("#266489");
                }

                entries[i] = new Microcharts.Entry(rndm)
                {
                    Label = "0" + i + "/02/2020",
                    ValueLabel = rndm.ToString(),
                    Color = color
                };
            }

            var chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Square
            };
            priceChart.Chart = chart;
        }

        private void AddCategory_Clicked(object sender, EventArgs e)
        {

        }

        private void Picture_Clicked(object sender, EventArgs e)
        {

        }
    }
}