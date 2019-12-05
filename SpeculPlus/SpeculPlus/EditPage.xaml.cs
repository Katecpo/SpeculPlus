using Logic;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeculPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
        private Product p = null;
        private IProductStorage storage;

		public EditPage ()
		{
			InitializeComponent ();
		}

        // ToDo: Faire le binding
        public EditPage(Product p, IProductStorage storage)
        {
            InitializeComponent();
            this.p = p;
            BindingContext = this.p;
            this.storage = storage;
        }

        // ToDo: Refresh UI
        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            p.Name = name.Text;
            p.Price = float.Parse(price.Text);
            p.Category = (Category)listCat.SelectedItem;

            storage.Update(p);

            await Navigation.PopAsync();
        }

    }
}