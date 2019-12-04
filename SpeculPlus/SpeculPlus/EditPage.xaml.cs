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

		public EditPage ()
		{
			InitializeComponent ();
		}

        public EditPage(Product p)
        {
            InitializeComponent();
            this.p = p;
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            p.Name = name.Text;
            p.Price = float.Parse(price.Text);
            p.Category = (Category)listCat.SelectedItem;
        }

    }
}