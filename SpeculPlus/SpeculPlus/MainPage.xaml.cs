using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Logic;
using FileStorage;
using System.Collections.Generic;

namespace SpeculPlus
{
    public partial class MainPage : ContentPage
    {
        private IProductStorage storage;
        private CategoryList cl;
        private CategoryListViewModel clvm;

        public MainPage()
        {
            InitializeComponent();

            storage = new JsonStorage("liste16");
            cl = storage.Load();
            clvm = new CategoryListViewModel(cl);

            BindingContext = clvm;
        }

        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            var overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = false,
                TopText = "Scannez votre produit",
                BottomText = "Alignez le code-barre"
            };
            overlay.BindingContext = overlay;
            var scanPage = new ZXingScannerPage(null, overlay);

            // Navigation au scanner
            await Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Création du produit après scan
                    ProductViewModel p = new ProductViewModel(new Product())
                    {
                        Barcode = result.Text,
                        Name = "Nouveau produit"
                    };

                    await Navigation.PushAsync(new EditPage(p, storage, clvm.DefaultCategory));
                });
            };
        }

        private async void ListeProduits_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProductViewModel ItemTapped = e.Item as ProductViewModel;
            await Navigation.PushAsync(new EditPage(ItemTapped, storage));
        }

        private void AddProduct(object sender, EventArgs e)
        {
            ProductViewModel p = new ProductViewModel(new Product());
            p.Name = "Test produit";
            p.Price = 6.5f;

            if (clvm.Categories.Count == 0)
            {
                clvm.Add(new Category("Figurines", "Black"));
                clvm.Add(new Category("Livres", "DarkGray"));
                clvm.Add(new Category("Autres", "Cyan"));
            }

            clvm.DefaultCategory.Add(p.Product);
        }

        private void SaveList(object sender, EventArgs e)
        {
            storage.Save();
        }

        /*
        private void Supprimer_Item(object sender, EventArgs e)
        {
            var ItemTapped = ((sender as MenuItem).BindingContext as ProductViewModel);

            storage.Delete(ItemTapped.Product);
            plvm.Remove(ItemTapped);
        }
        */
    }
}
