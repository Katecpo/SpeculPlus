using Logic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FileStorage
{
    /// <summary>
    /// Stockage en JSON
    /// </summary>
    public class JsonStorage : IProductStorage
    {
        private string file;
        private ProductList pl;

        /// <summary>
        /// Initialise le stockage dans un fichier JSON
        /// </summary>
        /// <param name="file">Chemin vers le fichier JSON</param>
        public JsonStorage(string file)
        {
            this.file = file;
        }

        public Product Create()
        {
            return new Product();
        }

        public void Delete(Product p)
        {
            Save();
        }

        public ProductList Load()
        {
            try
            {
                using (FileStream flux = new FileStream(file, FileMode.Open))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProductList));
                    pl = ser.ReadObject(flux) as ProductList;
                }
            }
            catch
            {
                pl = new ProductList();
            }

            return pl;
        }

        public void Update(Product p)
        {
            Save();
        }

        /// <summary>
        /// Sauvegarde un fichier Json
        /// </summary>
        private void Save()
        {
            using (FileStream flux = new FileStream(file, FileMode.Create))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProductList));
                ser.WriteObject(flux, pl);
            }
        }
    }
}
