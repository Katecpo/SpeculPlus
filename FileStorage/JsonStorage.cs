using Logic;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FileStorage
{
    /// <summary>
    /// Stockage en JSON
    /// </summary>
    public class JsonStorage : IProductStorage
    {
        #region Attributes
        private string file;
        private CategoryList cl;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialise le stockage dans un fichier JSON
        /// </summary>
        /// <param name="file">Nom du fichier (sans extension)</param>
        public JsonStorage(string file)
        {
            this.file = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), file + ".json");
        }
        #endregion

        #region Methods
        public Category Create()
        {
            return new Category("Nouvelle catégorie", "White");
        }

        public CategoryList Load()
        {
            try
            {
                using (FileStream flux = new FileStream(file, FileMode.Open))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CategoryList));
                    cl = ser.ReadObject(flux) as CategoryList;
                }
            }
            catch
            {
                cl = new CategoryList();
            }

            return cl;
        }

        public void Save()
        {
            using (FileStream flux = new FileStream(file, FileMode.Create))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CategoryList));
                ser.WriteObject(flux, cl);
            }
        }
        #endregion
    }
}
