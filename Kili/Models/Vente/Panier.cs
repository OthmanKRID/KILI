using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.Vente
{
    public class Panier
    {
        
        public string ArticleID { get; set; }
        public string PanierID { get; set; }

        public int Quantite { get; set; }

        public DateTime DateCreation { get; set; }

        public int ProduitID { get; set; }

        public virtual Produit Produit { get; set; }
    }
}
