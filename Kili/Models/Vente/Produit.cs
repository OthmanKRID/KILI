using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Vente
{
    public class Produit
    {
        [ScaffoldColumn(false)]
        public int ProduitID { get; set; }

        [Required, StringLength(100), Display(Name = "Désignation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Format")]
        public string Format { get; set; }

        [Required, StringLength(10000), Display(Name = "Déscription"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Prix")]
        public double PrixUnitaire { get; set; }

        [Required]
        [Display(Name = "Devise")]
        public string Devise { get; set; }

        public int CatalogueID { get; set; }

        public virtual Catalogue Catalogue { get; set; }
    }
}

