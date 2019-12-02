using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Product
    {
        private Category category;
        private String name;
        private float price;
        private String barcode;

        /// <summary>
        /// Getter & Setter for category
        /// </summary>
        public Category Category
        {
            get => category;
            set => category = value;
        }

        /// <summary>
        /// Getter & Setter for name
        /// </summary>
        public String Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Getter & Setter for price
        /// </summary>
        public float Price
        {
            get => price;
            set => price = value;
        }

        /// <summary>
        /// Getter & Setter for barcode
        /// </summary>
        public String Barcode
        {
            get => barcode;
            set => barcode = value;
        }
    }
}
