using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Logic;
using FileStorage;

namespace SpeculPlus
{
    public partial class MainPage : ContentPage
    {
        private IProductStorage storage;
        private ProductList pl;
        private ProductListViewModel plvm;

        public MainPage()
        {
            InitializeComponent();

            storage = new JsonStorage("products.json");
            pl = storage.Load();
            plvm = new ProductListViewModel(pl);
            BindingContext = plvm;
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
                    await Navigation.PopAsync();

                    // Création du produit après scan
                    ProductViewModel p = new ProductViewModel(storage.Create());
                    p.Barcode = result.Text;


                    plvm.Add(p.Product);
                    storage.Update(p.Product);

                    await Navigation.PushAsync(new EditPage(p, storage));
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
            ProductViewModel p = new ProductViewModel(storage.Create());
            plvm.Add(p.Product);
            
            p.Name = "Test produit";
            p.Price = 6.5f;
        }
    }
}
