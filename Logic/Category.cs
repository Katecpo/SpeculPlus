using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class Category
    {
        [DataMember] private string name;
        [DataMember] private string color;

        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Couleur de la catégorie
        /// </summary>
        public string Color
        {
            get => color;
            set => color = value;
        }
    }
}
