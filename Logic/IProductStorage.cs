namespace Logic
{
    /// <summary>
    /// Couche d'abstraction et de liaison entre la couche métier et la couche de stockage
    /// </summary>
    public interface IProductStorage
    {
        /// <summary>
        /// Crée une catégorie vide
        /// </summary>
        /// <returns>La catégorie créée</returns>
        Category Create();

        /// <summary>
        /// Charge une liste de catégories ainsi que ses produits
        /// </summary>
        /// <returns>Liste de catégorie stockée</returns>
        CategoryList Load();

        /// <summary>
        /// Sauvegarde la liste de catégories ainsi que ses produits
        /// </summary>
        void Save();
    }
}
