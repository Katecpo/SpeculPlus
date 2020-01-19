using Logic;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace SpeculPlus
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product product;
        private CategoryViewModel category;

        /// <summary>
        /// Crée le vue-modèle d'un produit
        /// </summary>
        /// <param name="p">Le modèle du produit</param>
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
        /// Le chemin de l'image du produit
        /// </summary>
        public string ImagePath
        {
            get => product.ImagePath;
            set
            {
                product.ImagePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImagePath"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
            }
        }

        /// <summary>
        /// L'image du produit
        /// </summary>
        public ImageSource ImageSource
        {
            get => product.ImageSource;
        }

        /// <summary>
        /// La catégorie du produit
        /// </summary>
        public CategoryViewModel Category
        {
            get => category;
            set
            {
                if (product.Category != null)
                {
                    category.Remove(this);
                }
                value.Add(this);

                product.Category = value.Category;
                category = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category"));
            }
        }

        /// <summary>
        /// La quantité du produit
        /// </summary>
        public int Quantity 
        { 
            get => product.Quantity;
            set
            {
                product.Quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        /// <summary>
        /// Le produit du vue-modèle
        /// </summary>
        public Product Product { get => product; }

        /// <summary>
        /// Liste de valeurs du prix du produit
        /// </summary>
        public Dictionary<string, float> PriceEvolution { get => product.PriceEvolution;
            set
            {
                product.PriceEvolution = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PriceEvolution"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Chart"));
            }
        }

        /// <summary>
        /// Graphique généré depuis la liste des prix
        /// </summary>
        public Chart Chart
        {
            get
            {
                if (PriceEvolution.Count != 0)
                {
                    int nValues = PriceEvolution.Count;
                    Microcharts.Entry[] entries = new Microcharts.Entry[nValues];
                    int i = 0;

                    foreach (var value in PriceEvolution)
                    {
                        float price = value.Value;
                        string date = value.Key;

                        float avg = 0;
                        int length = 0;
                        foreach (var j in entries)
                            if (j != null)
                            {
                                length += 1;
                                avg += j.Value;
                            }

                        avg /= length;

                        SKColor color;
                        if (price > avg * 1.25) // Bon
                        {
                            color = SKColor.Parse("#196b3f");
                        }
                        else if (price > avg * 1.75) // Très bon
                        {
                            color = SKColor.Parse("#00d964");
                        }
                        else if (price < avg * 0.75) // Mauvais
                        {
                            color = SKColor.Parse("#d96500");
                        }
                        else if (price < avg * 0.25) // Très mauvais
                        {
                            color = SKColor.Parse("#991700");
                        }
                        else // Moyenne
                        {
                            color = SKColor.Parse("#266489");
                        }

                        entries[i] = new Microcharts.Entry(price)
                        {
                            Label = date,
                            ValueLabel = price.ToString(),
                            Color = color
                        };

                        i++;
                    }

                    var chart = new LineChart()
                    {
                        Entries = entries,
                        LineMode = LineMode.Straight,
                        PointMode = PointMode.Square
                    };

                    return chart;
                }

                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
