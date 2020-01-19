using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Logic;
using FileStorage;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SpeculPlus
{
    public partial class MainPage : ContentPage
    {
        private IProductStorage storage;
        private CategoryList cl;
        private CategoryListViewModel clvm;
        private ObservableCollection<ProductsCategory> _expandedGroups;

        public MainPage()
        {
            InitializeComponent();

            storage = new JsonStorage("productlistf");
            cl = storage.Load();
            clvm = new CategoryListViewModel(cl);

            BindingContext = clvm;

            UpdateListContent();
        }

        #region Events
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
                    string scannedBarcode = result.Text;

                    // Crée les catégories par défaut si elles n'existent pas
                    clvm.CreateDefaultCategories();

                    // Si un produit a déjà ce code-barre alors on augmente juste sa quantité sinon on ajoute un nouveau produit
                    ProductViewModel existingProduct;
                    try
                    {
                        existingProduct = clvm.GetAllProducts().Single(i => i.Barcode == scannedBarcode);
                    }
                    catch (Exception)
                    {
                        existingProduct = null;
                    }

                    if (existingProduct != null)
                    {
                        existingProduct.Quantity += 1;
                        await Navigation.PushAsync(new EditPage(existingProduct, storage, clvm)); // Montrer le produit existant
                    } 
                    else
                    {
                        // Création du produit après scan
                        ProductViewModel p = new ProductViewModel(new Product())
                        {
                            Barcode = scannedBarcode,
                            Name = "Nouveau produit",
                            Category = clvm.DefaultCategory
                        };

                        await Navigation.PushAsync(new EditPage(p, storage, clvm, clvm.DefaultCategory)); // Editer le nouveau produit
                    }
                });
            };
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

            UpdateListContent();
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
                clvm.Add(new Category("Musique", "White"));
                clvm.Add(new Category("Autres", "Cyan"));
            }

            p.Category = clvm.DefaultCategory;
            //clvm.DefaultCategory.Add(p.Product);

            UpdateListContent();
        }

        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text != "")
            {
                var productsSearched = clvm.GetAllProducts().Where(c => c.Name.Contains(searchBar.Text));
                listeProduits.ItemsSource = productsSearched;
            }
            else
            {
                listeProduits.ItemsSource = clvm.ProductsCategory;
            }
        }

        private void HeaderTapped(object sender, EventArgs e)
        {
            // New page
            int selectedIndex = _expandedGroups.IndexOf(
                ((ProductsCategory)((Button)sender).CommandParameter));
            clvm.ProductsCategory[selectedIndex].Expanded = !clvm.ProductsCategory[selectedIndex].Expanded;

            UpdateListContent();
        }

        protected override void OnAppearing()
        {
            UpdateListContent();
        }

        private async void CategoryNameTapped(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Catégorie " + ((ProductsCategory)((Label)sender).BindingContext).Parent.Name, "Retour", "Supprimer");

            switch (action)
            {
                case "Supprimer":
                    CategoryViewModel catSelected = ((ProductsCategory)((Label)sender).BindingContext).Parent;

                    string confirm = await DisplayActionSheet("Êtes-vous sûr ?", "Non", "Oui");

                    if (confirm == "Oui")
                    {
                        clvm.Remove(catSelected);
                        UpdateListContent();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Met à jour la liste de produits
        /// </summary>
        private void UpdateListContent()
        {
            _expandedGroups = new ObservableCollection<ProductsCategory>();
            foreach (ProductsCategory category in clvm.ProductsCategory)
            {
                ProductsCategory newCat = new ProductsCategory(category.Parent, new List<ProductViewModel>(), category.Expanded);
                newCat.ProductCount = category.Count;

                if (category.Expanded)
                    foreach (ProductViewModel p in category)
                        newCat.Add(p);

                _expandedGroups.Add(newCat);
            }
            listeProduits.ItemsSource = _expandedGroups;
        }
        #endregion
    }
}
