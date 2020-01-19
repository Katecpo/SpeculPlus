using Logic;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Plugin.Media;

namespace SpeculPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
        private ProductViewModel p = null;
        private IProductStorage storage;
        private CategoryViewModel cvm = null;
        private CategoryListViewModel clvm = null;

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
            this.clvm = clvm;

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

        private async void AddCategory_Clicked(object sender, EventArgs e)
        {
            string newCategoryName = await DisplayPromptAsync("Nouvelle catégorie", "Quel devra être le nom de votre nouvelle catégorie ?", "Créer", "Annuler", "Nom de catégorie");

            if (newCategoryName != null)
            {
                bool canCreate = true;

                foreach (var c in clvm.Categories)
                    if (c.Name == newCategoryName)
                        canCreate = false;

                if (canCreate)
                {
                    clvm.Add(new Category(newCategoryName, "black"));
                }
                else
                {
                    await DisplayAlert("Échec", "Cette catégorie existe déjà !", "Zut");
                }
            }
        }

        private async void Picture_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Pas de caméra", ":( aucune caméra n'est disponible.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "ProductsImages",
                Name = p.GetHashCode() + ".jpg",
                AllowCropping = true,
                CompressionQuality = 80
                //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");

            p.ImagePath = file.Path;
        }
    }
}