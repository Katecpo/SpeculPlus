using Logic;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeculPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
        private ProductViewModel p = null;
        private IProductStorage storage;

		public EditPage ()
		{
			InitializeComponent ();
		}
        
        public EditPage(ProductViewModel p, IProductStorage storage)
        {
            InitializeComponent();

            this.p = p;
            BindingContext = this.p;
            this.storage = storage;
        }
        
        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            //p.Name = name.Text;
            //p.Price = float.Parse(price.Text, CultureInfo.InvariantCulture);
            //p.Category = (Category)listCat.SelectedItem;

            storage.Update(p.Product);

            await Navigation.PopToRootAsync();
        }

    }
}