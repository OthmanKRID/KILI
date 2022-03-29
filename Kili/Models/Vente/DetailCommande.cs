namespace Kili.Models.Vente
{
    public class DetailCommande
    {
        public int DetailCommandeID { get; set; }
        public int CommandeID { get; set; }

        public int ProduitID { get; set; }

        public int Quantite { get; set; }
        public double? PrixUnitaire { get; set; }

        public Commande Commande { get; set; }

        public Produit Produit { get; set; }
    }
}
