using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Category
    {
        private String name;
        private String color;

        /// <summary>
        /// Getter & Setter for name
        /// </summary>
        public String Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Getter & Setter for color
        /// </summary>
        public String Color
        {
            get => color;
            set => color = value;
        }
    }
}
