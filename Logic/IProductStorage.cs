namespace Logic
{
    /// <summary>
    /// Couche d'abstraction et de liaison entre la couche métier et la couche de stockage
    /// </summary>
    public interface IProductStorage
    {
        /// <summary>
        /// Crée un produit
        /// </summary>
        /// <returns>Le produit créé</returns>
        Product Create();

        /// <summary>
        /// Mets à jour le produit
        /// </summary>
        /// <param name="p">Produit à mettre à jour</param>
        void Update(Product p);

        /// <summary>
        /// Supprime le produit
        /// </summary>
        /// <param name="p">Produit à supprimer</param>
        void Delete(Product p);

        /// <summary>
        /// Charge une liste de produits
        /// </summary>
        /// <returns>Liste de produits stockée</returns>
        ProductList Load();
    }
}
