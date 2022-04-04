using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Vente
{
    public class Livraison
    {
        public int LivraisonID { get; set; }

        [Required, StringLength(100), Display(Name = "Désignation")]
        public string LivraisonName { get; set; }

        [StringLength(10000), Display(Name = "Déscription"), DataType(DataType.MultilineText)]
        public string LivraisonDescription { get; set; }

        [Display(Name = "Prix")]
        public double LivraisonPrice { get; set; }

        [Display(Name = "Devise")]
        public string LivraisonDevise { get; set; }

        public int? CoordonneesAcheteurID { get; set; }
        public virtual CoordonneesAcheteur CoordonneesAcheteur { get; set; }
    }
}
