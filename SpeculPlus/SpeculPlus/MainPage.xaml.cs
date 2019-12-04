﻿using System;
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

        public MainPage()
        {
            InitializeComponent();

            /*
            ProductList pl = new ProductList();

            Product p1 = new Product();
            p1.Name = "Star Wars";
            p1.Price = 20;

            Product p2 = new Product();
            p2.Name = "Figurine";
            p2.Price = 40;

            pl.Add(p1);
            pl.Add(p2);

            listeProduits.ItemsSource = pl.ListAll();
            */

            storage = new JsonStorage("products.json");
            pl = storage.Load();
            listeProduits.ItemsSource = pl.ListAll();
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
                    //await DisplayAlert("Scanned Barcode", result.Text, "OK");

                    // Création du produit après scan
                    Product p = storage.Create();
                    p.Barcode = result.Text;
                    pl.Add(p);
                    storage.Update(p);
                    
                    await Navigation.PushAsync(new EditPage(p, storage));
                });
            };
        }

        private async void ListeProduits_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Product ItemTapped = e.Item as Product;
            //await DisplayAlert("Hop hop hop", "Tu as appuyé sur " + ItemTapped.Name, "OK");
            await Navigation.PushAsync(new EditPage(ItemTapped, storage));
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            listeProduits.BeginRefresh();
            pl = storage.Load();
            listeProduits.EndRefresh();
        }
    }
}
