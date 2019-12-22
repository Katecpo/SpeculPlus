﻿using Logic;
using System.ComponentModel;

namespace SpeculPlus
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product product;

        /// <summary>
        /// Crée le vue-modèle d'un produit
        /// </summary>
        /// <param name="p"></param>
        public ProductViewModel(Product p)
        {
            product = p;
        }

        /// <summary>
        /// Le nom du produit
        /// </summary>
        public string Name
        {
            get => product.Name;
            set
            {
                product.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// Le prix du produit
        /// </summary>
        public float Price
        {
            get => product.Price;
            set
            {
                product.Price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }

        /// <summary>
        /// Le code-barre du produit
        /// </summary>
        public string Barcode
        {
            get => product.Barcode;
            set
            {
                product.Barcode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Barcode"));
            }
        }

        /// <summary>
        /// L'image du produit
        /// </summary>
        public string Image
        {
            get => product.Image;
            set
            {
                product.Image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
            }
        }

        /// <summary>
        /// Le produit du vue-modèle
        /// </summary>
        public Product Product { get => product; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
