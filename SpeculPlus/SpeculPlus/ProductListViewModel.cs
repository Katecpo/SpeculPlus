using Logic;
using System.Collections.ObjectModel;

namespace SpeculPlus
{
    public class ProductListViewModel
    {
        private ObservableCollection<ProductViewModel> productList = new ObservableCollection<ProductViewModel>();
        private ProductList pl;

        public ProductListViewModel(ProductList pl)
        {
            this.pl = pl;
            foreach (Product p in pl.Products)
            {
                productList.Add(new ProductViewModel(p));
            }
        }

        public ObservableCollection<ProductViewModel> ProductList { get => productList; }

        public void Add(Product p)
        {
            pl.Add(p);
            ProductViewModel pvm = new ProductViewModel(p);
            productList.Add(pvm);
        }

        public void Remove(ProductViewModel pvm)
        {
            productList.Remove(pvm);
            pl.Remove(pvm.Product);
        }
    }
}
