using Logic;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.Linq;

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
                    storage.Save();
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

            storage.Save();
        }

        private void AddPrice_Clicked(object sender, EventArgs e)
        {
            popupAddPrice.IsVisible = true;
        }

        private void PopupTapped(object sender, EventArgs e)
        {
            popupAddPrice.IsVisible = false;
        }

        private async void AddPricePopup_Clicked(object sender, EventArgs e)
        {
            if (priceDate.Date.ToString() != "" && pricePopup.Text != "")
            {
                var pe = p.PriceEvolution;
                pe.Add(priceDate.Date.ToString("dd/MM/yyyy"), float.Parse(pricePopup.Text));
                p.PriceEvolution = pe;

                popupAddPrice.IsVisible = false;
            }
            else 
            {
                await DisplayAlert("Échec", "Vous devez remplir les champs afin d'ajouter un prix", "Ok");
            }
        }
    }
}