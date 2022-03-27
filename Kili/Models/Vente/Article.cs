namespace Kili.Models.Vente
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int Quantite { get; set; }
        public int ProduitID { get; set; }
        public virtual Produit Produit { get; set; }
        public int PanierID { get; set; }
        public virtual Panier Panier { get; set; }
    }
}
