using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Logic;

namespace SpeculPlus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
        private Product product;
		public EditPage (CategoryList c, String barcode)
		{
			InitializeComponent ();
            this.listCat.ItemsSource = c.GetAll();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            product.Name = name.Text;
            product.Price = float.Parse(price.Text);
            product.Category = (Category)listCat.SelectedItem;
        }
    }
}