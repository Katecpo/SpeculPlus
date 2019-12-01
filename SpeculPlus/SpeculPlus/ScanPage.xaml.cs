using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SpeculPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
        ZXingScannerView zxing;

        public ScanPage() : base()
		{
            //initialisation de la View ZsingScanner
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            // Dès que le capteur trouve un Qr code, code barre, ...
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {
                    // Stop l'analyse 
                    zxing.IsAnalyzing = false;
                    // Ouvre une Pop-up affichant le texte, lien ...
                    await DisplayAlert("Résultat", result.Text, "OK");
                    await Navigation.PopAsync();
                });

            //Création de la Grid
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            //Ajout du composant dans la Grid
            grid.Children.Add(zxing);
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Scan
            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            // Ne scan plus
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}