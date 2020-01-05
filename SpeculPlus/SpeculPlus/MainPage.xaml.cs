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

                    await Navigation.PushAsync(new EditPage(p, storage, clvm.DefaultCategory, clvm));
                });
            };
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

        private void listeProduits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*listeProduits.SelectedItem = null;
            EditButton.IsVisible = DeleteButton.IsVisible = false;*/

            EditButton.IsVisible = DeleteButton.IsVisible = true;

            var animationEdit = new Animation(callback: d => EditButton.Rotation = d,
                                  start: EditButton.Rotation,
                                  end: EditButton.Rotation + 360,
                                  easing: Easing.SpringOut);
            animationEdit.Commit(EditButton, "Loop", length: 800);

            var animationDelete = new Animation(callback: d => DeleteButton.Rotation = d,
                                      start: DeleteButton.Rotation,
                                      end: DeleteButton.Rotation + 360,
                                      easing: Easing.SpringOut);
            animationDelete.Commit(DeleteButton, "Loop", length: 800);
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(listeProduits.SelectedItem as ProductViewModel, storage, clvm));
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var curItem = listeProduits.SelectedItem as ProductViewModel;

            curItem.Category.Remove(curItem);
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
